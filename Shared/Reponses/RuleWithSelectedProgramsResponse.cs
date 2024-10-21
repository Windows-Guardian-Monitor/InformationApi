using ClientServer.Shared.Database.Models;
using System.Net;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Reponses
{
	public class RuleWithSelectedProgramsResponse : StandardResponse
	{
		public RuleWithSelectedProgramsResponse(string ruleName, List<DbRuleProgram> programsWithSelectedOnes, int ruleId, string message, bool success, HttpStatusCode code) : base(message, success, code)
		{
			ProgramsWithSelectedOnes = programsWithSelectedOnes;
			RuleId = ruleId;
			RuleName = ruleName;
		}

		[JsonPropertyName("Programs")]
		public List<DbRuleProgram> ProgramsWithSelectedOnes { get; set; }

		[JsonPropertyName("RuleId")]
		public int RuleId { get; set; }

		[JsonPropertyName("RuleName")]
        public string RuleName { get; set; }
    }
}
