using System.Text.Json.Serialization;

namespace ClientServer.Shared.Requests
{
	public class GetRuleByWsRequest
	{
		[JsonPropertyName("Hostname")]
		public string Hostname { get; set; }
    }
}
