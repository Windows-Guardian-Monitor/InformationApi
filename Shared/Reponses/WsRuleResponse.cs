using ClientServer.Shared.Database.Models;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Reponses
{
	public class WsRuleResponse
	{
		public WsRuleResponse(List<DbRule> rules, bool success, string message)
		{
			Rules = rules;
			Success = success;
			Message = message;
		}

		[JsonPropertyName("Rules")]
        public List<DbRule> Rules { get; set; }

		[JsonPropertyName("Success")]
        public bool Success { get; set; }
		
		[JsonPropertyName("Message")]
		public string Message { get; set; }
	}
}
