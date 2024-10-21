using ClientServer.Shared.Requests.Events;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Requests.Performances
{
	public class OneDayPerformanceRequest
	{
		[JsonPropertyName("CustomDate")]
		public CustomDate CustomDate { get; set; }

		[JsonPropertyName("MachineName")]
        public string MachineName { get; set; }
    }
}
