using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Requests.Events
{
	public class ProcessFinishedEvent
	{
		[JsonIgnore]
		[Key]
		public int Id { get; set; }

		[JsonPropertyName("UserName")]
		public string UserName { get; set; }
		
		[JsonPropertyName("Domain")]
		public string Domain { get; set; }

		[JsonPropertyName("MachineName")]
		public string MachineName { get; set; }

		[JsonPropertyName("ProgramHash")]
		public string ProgramHash { get; set; }

		[JsonPropertyName("ProgramPath")]
		public string ProgramPath { get; set; }

		[JsonPropertyName("Timestamp")]
		public long Timestamp { get; set; }
	}
}
