using System.Text.Json.Serialization;

namespace ClientServer.Shared.DataTransferObjects.Authentication
{
    public class UserDto
    {
        [JsonPropertyName("UserName")]
        public string UserName { get; set; } = string.Empty;

        [JsonPropertyName("Password")]
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("IsAdmin")]
        public bool IsAdmin { get; set; }
    }
}
