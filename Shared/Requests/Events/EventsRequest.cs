using System.Text.Json.Serialization;

namespace ClientServer.Shared.Requests.Events
{
    public class EventsRequest
    {
        [JsonPropertyName("CustomDate")]
        public CustomDate CustomDate { get; set; }

        [JsonPropertyName("MachineName")]
        public string MachineName { get; set; }
    }
}
