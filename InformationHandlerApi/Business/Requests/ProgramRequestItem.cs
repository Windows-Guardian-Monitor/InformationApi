using ClientServer.Shared.Contracts;
using ClientServer.Shared.Database.Models;
using System.Text.Json.Serialization;

namespace InformationHandlerApi.Business.Requests
{
	public class ProgramRequestItem : IProgram
	{
		[JsonPropertyName("Path")]
		public string Path { get; set; }

		[JsonPropertyName("Name")]
		public string Name { get; set; }

		[JsonPropertyName("Hash")]
		public string Hash { get; set; }

		[JsonPropertyName("Hostname")]
		public string Hostname { get; set; }

		public static implicit operator DbProgram(ProgramRequestItem programRequest) => new DbProgram(programRequest.Path, programRequest.Name, programRequest.Hash, programRequest.Hostname);
	}
}
