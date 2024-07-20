using System.Net;

namespace InformationHandlerApi.Business.Responses
{
    public class StandardResponse
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
    }
}
