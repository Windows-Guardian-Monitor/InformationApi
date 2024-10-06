using System.Text.Json.Serialization;

namespace ClientServer.Shared.Requests
{
	public class DeleteRuleRequest
	{
		public DeleteRuleRequest(int ruleIdToDelete)
		{
			RuleIdToDelete = ruleIdToDelete;
		}
        public DeleteRuleRequest()
        {
            
        }

        [JsonPropertyName("RuleIdToDelete")]
		public int RuleIdToDelete { get; set; }
	}
}
