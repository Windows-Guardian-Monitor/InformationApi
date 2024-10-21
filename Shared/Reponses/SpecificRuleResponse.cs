using ClientServer.Shared.Database.Models;
using System.Net;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Reponses
{
	public class SpecificRuleResponse : StandardResponse
	{
		public SpecificRuleResponse(List<DbWorkstationSpecificRule> rules, string message, bool success, HttpStatusCode code) : base(message, success, code)
		{
			Rules = rules;
		}

		[JsonPropertyName("Rules")]
		public List<DbWorkstationSpecificRule> Rules { get; set; }
	}
}
