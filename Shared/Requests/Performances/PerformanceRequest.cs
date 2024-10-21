using ClientServer.Shared.Requests.Events;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Requests.Performances
{
	public class PerformanceRequest
	{
		[JsonPropertyName("MachineName")]
        public string MachineName { get; set; }
    }
}
