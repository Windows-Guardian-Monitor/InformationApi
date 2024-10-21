using System.Net;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Reponses
{
	public class LoginResponse : StandardResponse
	{
		public LoginResponse(string message, bool success, HttpStatusCode code, string jwtToken, string role, string userName, bool requestPasswordChange) : base(message, success, code)
		{
			JwtToken = jwtToken;
			Role = role;
			UserName = userName;
			RequestPasswordChange = requestPasswordChange;
		}

		public LoginResponse() : base("", true, HttpStatusCode.OK)
		{

		}

		public static LoginResponse Create(string token, string userName, string role, bool requestPasswordChange,StandardResponse standardResponse)
		{
			return new LoginResponse(standardResponse.Message, standardResponse.Success, standardResponse.Code, token, role, userName, requestPasswordChange);
		}

		[JsonPropertyName("JwtToken")]
		public string JwtToken { get; set; }

		[JsonPropertyName("Role")]
		public string Role { get; set; }

		[JsonPropertyName("UserName")]
		public string UserName { get; set; }

		[JsonPropertyName("RequestPasswordChange")]
        public bool RequestPasswordChange { get; set; }
    }
}
