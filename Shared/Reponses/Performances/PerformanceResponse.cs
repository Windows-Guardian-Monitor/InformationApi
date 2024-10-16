using ClientServer.Shared.Models;
using System.Net;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Reponses.Performances
{
	public class PerformanceResponse : StandardResponse
	{
		public PerformanceResponse(
			List<CpuPerformanceModel> cpuPerformances, List<RamPerformanceModel> ramPerformances, string message, bool success, HttpStatusCode code) : base(message, success, code)
		{
			CpuPerformances = cpuPerformances;
			RamPerformances = ramPerformances;
		}

		[JsonPropertyName("CpuPerformances")]
		public List<CpuPerformanceModel> CpuPerformances { get; set; }

		[JsonPropertyName("RamPerformances")]
		public List<RamPerformanceModel> RamPerformances { get; set; }
	}
}
