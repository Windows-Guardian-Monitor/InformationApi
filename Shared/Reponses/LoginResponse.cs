using InformationHandlerApi.Business.Responses;
using System.Net;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Reponses
{
	public class LoginResponse : StandardResponse
	{
		public LoginResponse(string message, bool success, HttpStatusCode code, string jwtToken) : base(message, success, code)
		{
			JwtToken = jwtToken;
		}

		public LoginResponse(string jwtToken, StandardResponse standardResponse) : base(standardResponse.Message, standardResponse.Success, standardResponse.Code)
		{
			JwtToken = jwtToken;
		}

        public LoginResponse() : base(string.Empty, true, HttpStatusCode.OK)
		{
            
        }

        [JsonPropertyName("JwtToken")]
        public string JwtToken { get; set; }
    }
}
