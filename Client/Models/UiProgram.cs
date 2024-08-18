using ClientServer.Shared.Contracts;
using System.Text.Json.Serialization;

namespace ClientServer.Client.Models
{
	public class UiProgram : IProgram
	{
		[JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Hash")]
		public string Hash { get; set; }
		
		[JsonPropertyName("Name")]
		public string Name { get; set; }
		
		[JsonPropertyName("Path")]
		public string Path { get; set; }

		[JsonIgnore]
        public bool Selected { get; set; }
    }
}
