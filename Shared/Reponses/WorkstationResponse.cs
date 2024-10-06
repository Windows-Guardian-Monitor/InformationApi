using ClientServer.Shared.DataTransferObjects;
using InformationHandlerApi.Business.Responses;
using System.Net;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.Reponses
{
	public class WorkstationResponse : StandardResponse
	{
		public WorkstationResponse(WorkstationItem workstation, string message, bool success, HttpStatusCode code) : base(message, success, code)
		{
			Workstation = workstation;
		}

		public static WorkstationResponse Create(WorkstationItem workstationItem, StandardResponse standardResponse) => new WorkstationResponse(workstationItem, standardResponse.Message, standardResponse.Success, standardResponse.Code);

		[JsonPropertyName("Workstation")]
		public WorkstationItem Workstation { get; set; }

	}
}
