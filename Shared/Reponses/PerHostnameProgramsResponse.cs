using ClientServer.Shared.Database.Models;
using System.Net;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Reponses
{
	public class PerHostnameProgramsResponse : StandardResponse
	{
		public PerHostnameProgramsResponse(List<DbProgram> programs, string message, bool success, HttpStatusCode code) : base(message, success, code)
		{
			Programs = programs;
		}

		[JsonPropertyName("Programs")]
		public List<DbProgram> Programs { get; set; }
	}
}
