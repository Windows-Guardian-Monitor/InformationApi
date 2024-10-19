using System.Net;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Reponses.Performances
{
	public class OneDayPerformanceResponse : StandardResponse
	{
		public OneDayPerformanceResponse(List<int> cpuPerformancesOfDay, List<int> ramPerformancesOfDay, string message, bool success, HttpStatusCode code) : base(message, success, code)
		{
			CpuPerformancesOfDay = cpuPerformancesOfDay;
			RamPerformancesOfDay = ramPerformancesOfDay;
		}

		[JsonPropertyName("CpuPerformancesOfDay")]
        public List<int> CpuPerformancesOfDay { get; set; }
        
        [JsonPropertyName("RamPerformancesOfDay")]
        public List<int> RamPerformancesOfDay { get; set; }
    }
}
