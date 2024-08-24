using System.Text.Json.Serialization;

namespace ClientServer.Shared.DataTransferObjects
{
	public class UserLoginDto
	{
        [JsonPropertyName("UserName")]
        public string UserName { get; set; } = string.Empty;

        [JsonPropertyName("Password")]
        public string Password { get; set; } = string.Empty;
    }
}
