using ClientServer.Shared.Contracts.Repositories;
using ClientServer.Shared.Database.Models;
using ClientServer.Shared.Database.Repositories;
using ClientServer.Shared.Reponses;
using ClientServer.Shared.Requests;
using ClientServer.Shared.Requests.Contracts;
using ClientServer.Shared.Requests.Rules;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace InformationHandlerApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RulesController : Controller
	{
		private readonly IRuleRepository _ruleRepository;
		private readonly IProgramRepository _programRepository;
		private readonly WorkstationRulesRepository _workstationRulesRepository;
		private readonly IWindowsWorkstationRepository _windowsWorkstationRepository;

		public RulesController(
			IRuleRepository ruleRepository,
			IProgramRepository programRepository,
			WorkstationRulesRepository workstationRulesRepository,
			IWindowsWorkstationRepository windowsWorkstationRepository)
		{
			_ruleRepository = ruleRepository;
			_programRepository = programRepository;
			_workstationRulesRepository = workstationRulesRepository;
			_windowsWorkstationRepository = windowsWorkstationRepository;
		}

		private (bool, StandardResponse?) Validate(IAddOrUpdateRuleRequest addOrUpdateRuleRequest)
		{
			if (addOrUpdateRuleRequest is null)
			{
				return (false, new StandardResponse("Não foi possível obter as regras enviadas na requisição", false, HttpStatusCode.BadRequest));
			}

			if (addOrUpdateRuleRequest.SelectedPrograms.Count is 0)
			{
				return (false, new StandardResponse("Não há programas cadastrados nesta regra", false, HttpStatusCode.BadRequest));
			}

			return (true, null);
		}

		[HttpPost("Create")]
		public StandardResponse CreateRules([FromBody] byte[] serializedProgramList)
		{
			try
			{
				var createRuleRequest = JsonSerializer.Deserialize<CreateRuleRequest>(serializedProgramList);

				var (valid, response) = Validate(createRuleRequest);

				if (valid is false)
				{
					return response;
				}

				foreach (var item in createRuleRequest.SelectedPrograms)
				{
					item.ProgramId = 0;
				}

				_ruleRepository.Insert(new DbRule(createRuleRequest.RuleName, createRuleRequest.SelectedPrograms));

				return StandardResponse.CreateOkResponse();
			}
			catch (Exception e)
			{
				return StandardResponse.CreateInternalServerErrorResponse(e.Message);
			}
		}

		[HttpPost("CreateForWorkstation")]
		public StandardResponse CreateNewRuleForSpecificWorkstation([FromBody] string serializedSpecificRuleRequest)
		{
			try
			{
				var wsSpecificRuleRequest = JsonSerializer.Deserialize<CreateWsSpecificRuleRequest>(serializedSpecificRuleRequest);

				var (valid, response) = ValidateWsSpecificRule(wsSpecificRuleRequest);

				if (valid is false)
				{
					return response;
				}

				var programs = wsSpecificRuleRequest.Programs.Select(p => new WorkstationSpecificDbRuleProgram()
				{
					Hash = p.Hash,
					Name = p.Name,
					Path = p.Path
				}).ToList();

				var workstations = wsSpecificRuleRequest.Workstations.Select(w => new WorkstationSpecificDbRuleWorkstation()
				{
					Hostname = w.HostName
				}).ToList();

				var dbRule = new DbWorkstationSpecificRule(programs, workstations, wsSpecificRuleRequest.RuleName);

				_workstationRulesRepository.Insert(dbRule);

				return StandardResponse.CreateOkResponse();
			}
			catch (Exception e)
			{
				return StandardResponse.CreateInternalServerErrorResponse(e.Message);
			}
		}

		private (bool, StandardResponse) ValidateWsSpecificRule(CreateWsSpecificRuleRequest req)
		{
			if (string.IsNullOrEmpty(req.RuleName))
			{
				return (false, StandardResponse.CreateBadRequest("A regra precisa ter um nome"));
			}

			if (req.Workstations.Count is 0)
			{
				return (false, StandardResponse.CreateBadRequest("A regra precisa ter pelo menos uma máquina cadastrada"));
			}

			if (req.Programs.Count is 0)
			{
				return (false, StandardResponse.CreateBadRequest("A regra precisa ter pelo menos uma aplicação cadastrada"));
			}

			return (true, StandardResponse.CreateOkResponse());
		}

		[HttpPost("Update")]
		public StandardResponse UpdateRules([FromBody] byte[] serializedProgramList)
		{
			try
			{
				var createRuleRequest = JsonSerializer.Deserialize<UpdateRuleRequest>(serializedProgramList);

				var (valid, response) = Validate(createRuleRequest);

				if (valid is false)
				{
					return response;
				}

				foreach (var item in createRuleRequest.SelectedPrograms)
				{
					item.ProgramId = 0;
				}

				_ruleRepository.DeleteById(createRuleRequest.RuleId);

				_ruleRepository.Insert(new DbRule(createRuleRequest.RuleName, createRuleRequest.SelectedPrograms));

				return StandardResponse.CreateOkResponse();
			}
			catch (Exception e)
			{
				return StandardResponse.CreateInternalServerErrorResponse(e.Message);
			}
		}

		[HttpPost("UpdateSpecificRule")]
		public StandardResponse UpdateSpecificRule([FromBody] string serializedProgramList)
		{
			try
			{
				var existentRuleRequest = JsonSerializer.Deserialize<RuleWithSelectedProgramsAndWorkstationsResponse>(serializedProgramList);

				if (existentRuleRequest is null)
				{
					return StandardResponse.CreateInternalServerErrorResponse("Não foi possível atualizar a regra");
				}

				if (existentRuleRequest.ProgramsWithSelectedOnes is null || existentRuleRequest.ProgramsWithSelectedOnes.Count is 0)
				{
					return StandardResponse.CreateInternalServerErrorResponse("Não foi possível obter as aplicações para atualizar a regra");
				}

				if (existentRuleRequest.WorkstationsWithSelectedOnes is null || existentRuleRequest.WorkstationsWithSelectedOnes.Count is 0)
				{
					return StandardResponse.CreateInternalServerErrorResponse("Não foi possível obter as máquinas para atualizar a regra");
				}


				var programs = existentRuleRequest.ProgramsWithSelectedOnes.Where(p => p.Selected).Select(p => new WorkstationSpecificDbRuleProgram()
				{
					Hash = p.Hash,
					Name = p.Name,
					Path = p.Path,
					Selected = p.Selected
				}).ToList();

				var workstations = existentRuleRequest.WorkstationsWithSelectedOnes.Where(w => w.Selected).Select(w => new WorkstationSpecificDbRuleWorkstation()
				{
					Hostname = w.Hostname,
					Selected = w.Selected
				}).ToList();

				_workstationRulesRepository.DeleteById(existentRuleRequest.RuleId);

				var dbRule = new DbWorkstationSpecificRule(programs, workstations, existentRuleRequest.RuleName);

				_workstationRulesRepository.Insert(dbRule);

				return StandardResponse.CreateOkResponse();
			}
			catch (Exception e)
			{
				return StandardResponse.CreateInternalServerErrorResponse(e.Message);
			}
		}

		[HttpPost("Acquire")]
		public RuleWithSelectedProgramsResponse GetRules([FromBody] byte[] serializedGetRuleByIdRequest)
		{
			try
			{
				var getRuleByIdRequest = JsonSerializer.Deserialize<GetRuleByIdRequest>(serializedGetRuleByIdRequest);

				var rule = _ruleRepository.GetById(getRuleByIdRequest.RuleId);

				var dbPrograms = _programRepository.GetAll();

				var businessPrograms = dbPrograms.Select(p => new DbRuleProgram(p.Path, p.Name, p.Hash)).ToList();

				foreach (var item in rule.Programs)
				{
					foreach (var programToSelect in businessPrograms)
					{
						if (programToSelect.Hash.Equals(item.Hash, StringComparison.OrdinalIgnoreCase))
						{
							programToSelect.Selected = true;
							break;
						}
					}
				}

				return new RuleWithSelectedProgramsResponse(rule.Name, businessPrograms, rule.RuleId, string.Empty, true, HttpStatusCode.OK);

			}
			catch (Exception e)
			{
				return new RuleWithSelectedProgramsResponse(string.Empty, null, 0, e.Message, false, HttpStatusCode.InternalServerError);
			}
		}

		[HttpPost("AcquireWorkstationRule")]
		public RuleWithSelectedProgramsAndWorkstationsResponse GetRules([FromBody] int workstationRuleByIdRequest)
		{
			try
			{
				var rule = _workstationRulesRepository.GetById(workstationRuleByIdRequest);

				if (rule is null)
				{
					throw new Exception("Houve um erro ao tentar atualizar a regra");
				}

				var dbPrograms = _programRepository.GetAll();

				var businessPrograms = dbPrograms.Select(p => new WorkstationSpecificDbRuleProgram(p.Path, p.Name, p.Hash)).ToList();

				foreach (var item in rule.Programs)
				{
					foreach (var programToSelect in businessPrograms)
					{
						if (programToSelect.Hash.Equals(item.Hash, StringComparison.OrdinalIgnoreCase))
						{
							programToSelect.Selected = true;
							break;
						}
					}
				}

				var dbWorkstations = _windowsWorkstationRepository.SelectWorkstations();

				var ruleWorkstations = dbWorkstations.Select(w => new WorkstationSpecificDbRuleWorkstation()
				{
					Hostname = w.HostName
				}).ToList();

				foreach (var item in ruleWorkstations)
				{
					foreach (var workstationToSelect in ruleWorkstations)
					{
						if (workstationToSelect.Hostname.Equals(item.Hostname, StringComparison.OrdinalIgnoreCase))
						{
							workstationToSelect.Selected = true;
							break;
						}
					}
				}

				return new RuleWithSelectedProgramsAndWorkstationsResponse(businessPrograms,
															   ruleWorkstations,
															   workstationRuleByIdRequest,
															   rule.RuleName,
															   string.Empty,
															   true,
															   HttpStatusCode.OK);

			}
			catch (Exception e)
			{
				return new RuleWithSelectedProgramsAndWorkstationsResponse(null, null, 0, string.Empty, e.Message, false, HttpStatusCode.InternalServerError);
			}
		}

		//WINDOWS
		[HttpPost("AcquireAll")]
		public WsRuleResponse GetAllRules(GetRuleByWsRequest getRuleByWsRequest)
		{
			try
			{
				List<DbRule> rules;

				var r = _workstationRulesRepository.GetByMachineName(getRuleByWsRequest.Hostname);

				if (r is not null && r.Count > 0)
				{
					rules = r.Select(r => new DbRule(r.RuleName, r.Programs.Select(p => new DbRuleProgram(p.Path, p.Name, p.Hash)).ToList())).ToList();
				}
				else
				{
					rules = _ruleRepository.GetAll();
				}

				return new WsRuleResponse(rules, true, string.Empty);
			}
			catch (Exception e)
			{
				return new WsRuleResponse(null, false, e.Message);
			}
		}

		[HttpPost("Delete")]
		public ValueTask<ActionResult<StandardResponse>> DeletedRuleById([FromBody] byte[] serializedDeleteRuleRequest)
		{
			try
			{
				var deleteRuleRequest = JsonSerializer.Deserialize<DeleteRuleRequest>(serializedDeleteRuleRequest);

				_ruleRepository.DeleteById(deleteRuleRequest.RuleIdToDelete);

				return new ValueTask<ActionResult<StandardResponse>>(StandardResponse.CreateOkResponse());
			}
			catch (Exception e)
			{
				return new ValueTask<ActionResult<StandardResponse>>(StandardResponse.CreateInternalServerErrorResponse(e.Message));
			}
		}

		[HttpPost("DeleteWorkstationRule")]
		public StandardResponse DeletedRuleById([FromBody] int id)
		{
			try
			{
				_workstationRulesRepository.DeleteById(id);

				return StandardResponse.CreateOkResponse();
			}
			catch (Exception e)
			{
				return StandardResponse.CreateInternalServerErrorResponse(e.Message);
			}
		}

		[HttpGet(Name = "GetGeneralRules")]
		public RuleResponse Get()
		{
			try
			{
				var rules = _ruleRepository.GetAll();

				return new RuleResponse(true, string.Empty, rules, HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				return new RuleResponse(false, e.Message, null, HttpStatusCode.InternalServerError);
			}
		}

		[HttpPost("GetSpecificRules")]
		public SpecificRuleResponse GetSpecificRules(object _)
		{
			try
			{
				var rules = _workstationRulesRepository.GetAll();

				if (rules.Count is 0)
				{
					return new SpecificRuleResponse(new List<DbWorkstationSpecificRule>(), "Não há regras cadastradas", true, HttpStatusCode.OK);
				}

				return new SpecificRuleResponse(rules, string.Empty, true, HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				return new SpecificRuleResponse(null, e.Message, false, HttpStatusCode.OK);
			}
		}
	}
}
