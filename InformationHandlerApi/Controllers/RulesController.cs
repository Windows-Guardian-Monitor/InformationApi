using ClientServer.Shared.Reponses;
using ClientServer.Shared.Requests;
using ClientServer.Shared.Requests.Contracts;
using InformationHandlerApi.Business.Responses;
using InformationHandlerApi.Contracts.Repositories;
using InformationHandlerApi.Database.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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

		public RulesController(IRuleRepository ruleRepository, IProgramRepository programRepository)
		{
			_ruleRepository = ruleRepository;
			_programRepository = programRepository;
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
	}
}
