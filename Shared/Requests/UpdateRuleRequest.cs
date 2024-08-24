using ClientServer.Shared.Requests.Contracts;
using InformationHandlerApi.Database.Models;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Requests
{
	public class UpdateRuleRequest : IAddOrUpdateRuleRequest
	{
		public UpdateRuleRequest(int ruleId, string ruleName, List<DbRuleProgram> selectedPrograms)
		{
			RuleName = ruleName;
			SelectedPrograms = selectedPrograms;
			RuleId = ruleId;
		}

		[JsonPropertyName("RuleId")]
		public int RuleId { get; set; }

		[JsonPropertyName("RuleName")]
		public string RuleName { get; set; }

		[JsonPropertyName("SelectedPrograms")]
		public List<DbRuleProgram> SelectedPrograms { get; set; }
	}
}
