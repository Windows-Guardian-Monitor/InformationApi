using InformationHandlerApi.Database.Models;
using System.Text.Json.Serialization;

namespace InformationHandlerApi.Business.Responses
{
	public class RuleResponse
	{
		public RuleResponse(bool sucess, string message, List<DbRule> rules)
		{
			Sucess = sucess;
			Message = message;
			Rules = rules;
		}

		[JsonPropertyName("Sucess")]
		public bool Sucess { get; set; }
		
		[JsonPropertyName("Message")]
		public string Message { get; set; }

		[JsonPropertyName("Rules")]
		public List<DbRule> Rules { get; set; }
    }
}
