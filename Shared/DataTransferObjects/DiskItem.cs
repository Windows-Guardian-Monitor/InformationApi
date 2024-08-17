using System.Text.Json.Serialization;

namespace ClientServer.Shared.DataTransferObjects
{
	public class DiskItem
	{
        [JsonPropertyName("totalSize")]
        public string? TotalSize { get; set; }

        [JsonPropertyName("diskType")]
        public string? DiskType { get; set; }
        
        [JsonPropertyName("diskName")]
        public string? DiskName { get; set; }
    }
}
