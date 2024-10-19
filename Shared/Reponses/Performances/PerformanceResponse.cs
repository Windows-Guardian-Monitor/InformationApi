using ClientServer.Shared.Models;
using System.Net;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Reponses.Performances
{
	public class PerformanceResponse : StandardResponse
	{
		public PerformanceResponse(
			CpuPerformanceModel cpuPerformances, RamPerformanceModel ramPerformances, string message, bool success, HttpStatusCode code) : base(message, success, code)
		{
			CpuPerformance = cpuPerformances;
			RamPerformance = ramPerformances;
		}

        public PerformanceResponse() : base(string.Empty, true, HttpStatusCode.OK)
        {
            
        }

        [JsonPropertyName("CpuPerformance")]
		public CpuPerformanceModel CpuPerformance { get; set; }

		[JsonPropertyName("RamPerformance")]
		public RamPerformanceModel RamPerformance { get; set; }
	}
}
