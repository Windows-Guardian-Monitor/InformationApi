using ClientServer.Shared.Database.Models.Authentication;
using System.Net;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Reponses
{
	public class UsersResponse : StandardResponse
	{
		public UsersResponse(List<DbUserWithoutPassword> users, string message, bool success, HttpStatusCode code) : base(message, success, code)
		{
			Users = users;
		}

		[JsonPropertyName("Users")]
        public List<DbUserWithoutPassword> Users { get; set; }
    }
}
