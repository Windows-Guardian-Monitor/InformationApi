using System.Text.Json.Serialization;

namespace ClientServer.Shared.Models
{
	public class RamPerformanceModel
	{
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("RamUsagePercentage")]
		public string RamUsagePercentage { get; set; } = string.Empty;

		[JsonPropertyName("DateTime")]
		public DateTime DateTime { get; set; } = DateTime.Now;

		[JsonPropertyName("MachineName")]
		public string MachineName { get; set; }
	}
}
