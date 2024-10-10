using InformationHandlerApi.Business.Requests.Events;
using InformationHandlerApi.Business.Responses;
using System.Net;

namespace ClientServer.Shared.Reponses
{
    public class EventsResponse : StandardResponse
    {
        public EventsResponse(List<ProcessFinishedEvent> events, string message, bool success, HttpStatusCode code) : base(message, success, code)
        {
            Events = events;
        }

        public List<ProcessFinishedEvent> Events { get; set; }
    }
}
