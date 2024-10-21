using ClientServer.Shared.Requests.Events;
using System.Net;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Reponses
{
	public class EventsResponse : StandardResponse
    {
        public EventsResponse(List<ProcessFinishedEvent> events, string message, bool success, HttpStatusCode code) : base(message, success, code)
        {
            Events = events;
        }

        [JsonPropertyName("Events")]
        public List<ProcessFinishedEvent> Events { get; set; }
    }
}
