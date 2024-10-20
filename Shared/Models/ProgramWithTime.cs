using System.Text.Json.Serialization;

namespace ClientServer.Shared.Models
{
	public class ProgramWithTime
	{
		public class DbProgramWithExecutionTime
		{
			[JsonPropertyName("Id")]
            public int Id { get; set; }

			[JsonPropertyName("Path")]
            public string Path { get; set; }
			
			[JsonPropertyName("Name")]
			public string Name { get; set; }
			
			[JsonPropertyName("ExecutionTime")]
			public DateTime ExecutionTime { get; set; } = DateTime.Now;
		}
	}
}
