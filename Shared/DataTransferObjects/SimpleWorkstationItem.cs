using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ClientServer.Shared.DataTransferObjects
{
	public class SimpleWorkstationItem
	{
		[JsonPropertyName("Id")]
        public int Id { get; set; }

		[JsonPropertyName("HostName")]
		public string HostName { get; set; }

		[JsonPropertyName("Uuid")]
		public string Uuid { get; set; }

		[JsonPropertyName("Selected")]
		[NotMapped]
		public bool Selected { get; set; }
	}
}
