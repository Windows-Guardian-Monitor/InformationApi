using ClientServer.Shared.DataTransferObjects;
using System.Net;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Reponses
{
	public class AllWorkstationsResponse : StandardResponse
	{
		public AllWorkstationsResponse(List<SimpleWorkstationItem> workstations, string message, bool success, HttpStatusCode code) : base(message, success, code)
		{
			Workstations = workstations;
		}

		[JsonPropertyName("Workstations")]
		public List<SimpleWorkstationItem> Workstations { get; set; }
    }
}
