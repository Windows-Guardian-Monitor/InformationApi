using ClientServer.Shared.Contracts;
using InformationHandlerApi.Database.Models;
using System.Text.Json.Serialization;

namespace InformationHandlerApi.Business.Requests
{
	public class ProgramRequest : IProgram
	{
		[JsonPropertyName("Path")]
		public string Path { get; set; }

		[JsonPropertyName("Name")]
		public string Name { get; set; }

		[JsonPropertyName("Hash")]
		public string Hash { get; set; }

		public static implicit operator DbProgram(ProgramRequest programRequest) => new DbProgram(programRequest.Path, programRequest.Name, programRequest.Hash);
	}
}
