using System.Text.Json.Serialization;

namespace ClientServer.Shared.Requests.User
{
	public class UserListRequest
	{
		[JsonPropertyName("IsRequestFromAdmin")]
        public bool IsRequestFromAdmin { get; set; }
    }
}
