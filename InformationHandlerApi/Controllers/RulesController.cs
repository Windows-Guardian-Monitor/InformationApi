using InformationHandlerApi.Business.Responses;
using InformationHandlerApi.Contracts.Repositories;
using InformationHandlerApi.Database.Models;
using Microsoft.AspNetCore.Mvc;
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
				var programs = JsonSerializer.Deserialize<List<DbRuleProgram>>(serializedProgramList);

				if (programs is null)
				{
					return new ValueTask<ActionResult<StandardResponse>>(new StandardResponse
					{
						Code = System.Net.HttpStatusCode.InternalServerError,
						Message = "Could not obtain workstation info"
					});
				}

				_ruleRepository.Insert(new DbRule(programs));

				return new ValueTask<ActionResult<StandardResponse>>(new StandardResponse
				{
					Code = System.Net.HttpStatusCode.OK,
					Message = "OK"
				});
			}
			catch (Exception e)
			{
				return new ValueTask<ActionResult<StandardResponse>>(new StandardResponse
				{
					Code = System.Net.HttpStatusCode.InternalServerError,
					Message = e.Message
				});
			}
		}

		[HttpPost("Acquire")]
		public ValueTask<ActionResult<RuleResponse>> GetRules([FromBody] byte[] serializedMachineUuid)
		{
			try
			{
				var uuid = JsonSerializer.Deserialize<string>(serializedMachineUuid);

				var rules = _ruleRepository.GetAll();

				return new ValueTask<ActionResult<RuleResponse>>(new RuleResponse(true, string.Empty, rules));
			}
			catch (Exception e)
			{
				return new ValueTask<ActionResult<RuleResponse>>(new RuleResponse(false, e.Message, new List<DbRule>()));
			}
		}
	}
}
