using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InformationHandlerApi.Business.Requests.Events
{
	public class ProcessFinishedEvent
	{
		[JsonIgnore]
		[Key]
		public int Id { get; set; }

		public string UserName { get; set; }
		public string Domain { get; set; }
		public string MachineName { get; set; }
		public string ProgramHash { get; set; }
		public string ProgramPath { get; set; }
		public long Timestamp { get; set; }
	}
}
