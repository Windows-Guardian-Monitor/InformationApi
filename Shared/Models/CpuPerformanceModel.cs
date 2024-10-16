using System.Text.Json.Serialization;

namespace ClientServer.Shared.Models
{
	public class CpuPerformanceModel
	{
		[JsonPropertyName("CpuUsagePercentage")]
		public string CpuUsagePercentage { get; set; } = string.Empty;

        [JsonPropertyName("DateTime")]
		public DateTime DateTime { get; set; } = DateTime.Now;

		[JsonPropertyName("MachineName")]
        public string MachineName { get; set; }
    }
}