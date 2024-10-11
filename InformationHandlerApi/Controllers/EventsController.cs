using ClientServer.Shared.Database.Repositories;
using ClientServer.Shared.Reponses;
using ClientServer.Shared.Requests.Events;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InformationHandlerApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EventsController : Controller
	{
		private readonly ProcessFinishedRepository _processFinishedRepository;
		public EventsController(ProcessFinishedRepository processFinishedRepository)
		{
			_processFinishedRepository = processFinishedRepository;
		}

		[HttpPost("SendEvents")]
		public ActionResult<StandardResponse> SendEvents([FromBody] string eventRequest)
		{
			try
			{
				var events = JsonSerializer.Deserialize<ProcessFinishedEvent[]>(eventRequest);

				if (events == null || events.Length is 0)
				{
					return StandardResponse.CreateOkResponse();
				}

				_processFinishedRepository.InsertMany(events);

				return StandardResponse.CreateOkResponse();
			}
			catch (Exception e)
			{
				return StandardResponse.CreateInternalServerErrorResponse(e.Message);
			}
		}

		[HttpPost("GetEvents")]
		public ActionResult<EventsResponse> GetEvents(EventsRequest eventsRequest)
		{
			try
			{
				var events = _processFinishedRepository.GetByDate(eventsRequest.CustomDate);

				return new EventsResponse(events, string.Empty, true, System.Net.HttpStatusCode.OK);
			}
			catch (Exception e)
			{
				return new EventsResponse(null, e.Message, false, System.Net.HttpStatusCode.InternalServerError);
			}
		}
	}
}
