using ClientServer.Shared.Database.Models;
using System.Net;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Reponses
{
	public class RuleResponse : StandardResponse
	{
		public RuleResponse(bool success, string message, List<DbRule> rules, HttpStatusCode code) : base(message, success, code)
		{
			Success = success;
			Message = message;
			Rules = rules;
		}

		[JsonPropertyName("Success")]
		public bool Success { get; set; }

		[JsonPropertyName("Message")]
		public string Message { get; set; }

		[JsonPropertyName("Rules")]
		public List<DbRule> Rules { get; set; }
	}
}
