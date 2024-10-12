using ClientServer.Shared.Database.Models;
using System.Net;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Reponses
{
	public class RuleWithSelectedProgramsAndWorkstationsResponse : StandardResponse
	{
		public RuleWithSelectedProgramsAndWorkstationsResponse(
			List<WorkstationSpecificDbRuleProgram> programsWithSelectedOnes,
			List<WorkstationSpecificDbRuleWorkstation> workstationsWithSelectedOnes,
			int ruleId,
			string ruleName,
			string message,
			bool success,
			HttpStatusCode code) : base(message, success, code)
		{
			ProgramsWithSelectedOnes = programsWithSelectedOnes;
			WorkstationsWithSelectedOnes = workstationsWithSelectedOnes;
			RuleId = ruleId;
			RuleName = ruleName;
		}

		[JsonPropertyName("Programs")]
		public List<WorkstationSpecificDbRuleProgram> ProgramsWithSelectedOnes { get; set; }

		[JsonPropertyName("Workstations")]
		public List<WorkstationSpecificDbRuleWorkstation> WorkstationsWithSelectedOnes { get; set; }

		[JsonPropertyName("RuleId")]
		public int RuleId { get; set; }

		[JsonPropertyName("RuleName")]
		public string RuleName { get; set; }
	}
}
