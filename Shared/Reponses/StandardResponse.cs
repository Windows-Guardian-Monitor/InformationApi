using System.Net;
using System.Text.Json.Serialization;

namespace InformationHandlerApi.Business.Responses
{
	public class StandardResponse
	{
		public StandardResponse(string message, bool success, HttpStatusCode code)
		{
			Message = message;
			Code = code;
			Success = success;
		}

		//public StandardResponse()
		//{

		//}

		[JsonPropertyName("Code")]
		public HttpStatusCode Code { get; set; }
		[JsonPropertyName("Message")]
		public string Message { get; set; }
		[JsonPropertyName("Success")]
		public bool Success { get; set; }

		public static StandardResponse CreateOkResponse() => new StandardResponse("OK", true, HttpStatusCode.OK);
		public static StandardResponse CreateInternalServerErrorResponse(string exceptionMessage) => new StandardResponse(exceptionMessage, false, HttpStatusCode.InternalServerError);
		public static StandardResponse CreateBadRequest(string message) => new StandardResponse(message, false, HttpStatusCode.BadRequest);
		public static StandardResponse CreateConflict(string message) => new StandardResponse(message, false, HttpStatusCode.Conflict);
	}
}
