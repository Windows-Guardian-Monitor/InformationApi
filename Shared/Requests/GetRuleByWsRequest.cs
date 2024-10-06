using System.Text.Json.Serialization;

namespace ClientServer.Shared.Requests
{
	public class GetRuleByWsRequest
	{
		[JsonPropertyName("MachineUuid")]
        public string MachineUuid { get; set; }
    }
}
