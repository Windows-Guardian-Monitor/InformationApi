using System.Text.Json.Serialization;

namespace ClientServer.Shared.Requests
{
	public class GetRuleByIdRequest
	{
		public GetRuleByIdRequest(int ruleId)
		{
			RuleId = ruleId;
		}

		[JsonPropertyName("RuleId")]
        public int RuleId { get; set; }
    }
}
