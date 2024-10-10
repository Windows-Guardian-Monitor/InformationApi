using InformationHandlerApi.Business.Requests.Events;
using InformationHandlerApi.Business.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InformationHandlerApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EventsController : Controller
	{
		
		public EventsController()
		{
			
		}

		[HttpPost("SendEvents")]
		public ActionResult<StandardResponse> SendEvents([FromBody] string eventRequest)
		{
			try
			{
				var events = JsonSerializer.Deserialize<ProcessFinishedEvent[]>(eventRequest);

				return StandardResponse.CreateOkResponse();
			}
			catch (Exception e)
			{
				return StandardResponse.CreateInternalServerErrorResponse(e.Message);
			}
		}
	}
}
