using ClientServer.Shared.Requests;
using InformationHandlerApi.Business.Responses;
using InformationHandlerApi.Contracts.Repositories;
using InformationHandlerApi.Database.Models;
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

		public RulesController(IRuleRepository ruleRepository)
		{
			_ruleRepository = ruleRepository;
		}

		[HttpPost("Create")]
		public ValueTask<ActionResult<StandardResponse>> CreateRules([FromBody] byte[] serializedProgramList)
		{
			try
			{
				var createRuleRequest = JsonSerializer.Deserialize<CreateRuleRequest>(serializedProgramList);

				if (createRuleRequest is null)
				{
					return new ValueTask<ActionResult<StandardResponse>>(new StandardResponse("Não foi possível obter as regras enviadas na requisição", false, HttpStatusCode.BadRequest));
				}

				if (createRuleRequest.SelectedPrograms.Count is 0)
				{
					return new ValueTask<ActionResult<StandardResponse>>(new StandardResponse("Não há programas cadastrados nesta regra", false, HttpStatusCode.BadRequest));
				}

				_ruleRepository.Insert(new DbRule(createRuleRequest.RuleName, createRuleRequest.SelectedPrograms));

				return new ValueTask<ActionResult<StandardResponse>>(StandardResponse.CreateOkResponse());
			}
			catch (Exception e)
			{
				return new ValueTask<ActionResult<StandardResponse>>(StandardResponse.CreateInternalServerErrorResponse(e.Message));
			}
		}

		[HttpPost("Acquire")]
		public ValueTask<ActionResult<RuleResponse>> GetRules([FromBody] byte[] serializedMachineUuid)
		{
			try
			{
				var uuid = JsonSerializer.Deserialize<string>(serializedMachineUuid);

				var rules = _ruleRepository.GetAll();

				var response = new RuleResponse(true, string.Empty, rules, HttpStatusCode.OK);

				return new ValueTask<ActionResult<RuleResponse>>(response);
			}
			catch (Exception e)
			{
				return new ValueTask<ActionResult<RuleResponse>>(new RuleResponse(false, e.Message, new List<DbRule>(), HttpStatusCode.InternalServerError));
			}
		}

		[HttpPost("Delete")]
		public ValueTask<ActionResult<StandardResponse>> DeletedRuleById ([FromBody] byte[] serializedDeleteRuleRequest)
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
