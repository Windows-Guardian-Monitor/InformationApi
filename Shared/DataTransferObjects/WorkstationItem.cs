using System.Text.Json.Serialization;

namespace ClientServer.Shared.DataTransferObjects
{
    
	public class WorkstationItem
	{
		public WorkstationItem(string? hostName, string? uuid, string? cpuDescription, string? cpuName, string? cpuManufacturer, string? cpuArchitecture, string? totalMemory, string? memorySpeed, string? memoryManufacturer, string? oSDescription, string? osManufacturer, string? osSerial, List<DiskItem> disks)
		{
			HostName = hostName;
			Uuid = uuid;
			CpuDescription = cpuDescription;
			CpuName = cpuName;
			CpuManufacturer = cpuManufacturer;
			CpuArchitecture = cpuArchitecture;
			TotalMemory = totalMemory;
			MemorySpeed = memorySpeed;
			MemoryManufacturer = memoryManufacturer;
			OSDescription = oSDescription;
			OsManufacturer = osManufacturer;
			OsSerial = osSerial;
			Disks = disks;
		}

		[JsonPropertyName("hostName")]
        public string? HostName { get; set; }
		
		[JsonPropertyName("uuid")]
        public string? Uuid { get; set; }
		
		[JsonPropertyName("cpuDescription")]
		public string? CpuDescription { get; set; }
		
		[JsonPropertyName("cpuName")]
        public string? CpuName { get; set; }
		
		[JsonPropertyName("cpuManufacturer")]
        public string? CpuManufacturer { get; set; }
		
		[JsonPropertyName("cpuArchitecture")]
        public string? CpuArchitecture { get; set; }
		
		[JsonPropertyName("totalMemory")]
        public string? TotalMemory { get; set; }
		
		[JsonPropertyName("memorySpeed")]
        public string? MemorySpeed { get; set; }

		[JsonPropertyName("memoryManufacturer")]
        public string? MemoryManufacturer { get; set; }
		
		[JsonPropertyName("osDescription")]
        public string? OSDescription { get; set; }		

		[JsonPropertyName("osManufacturer")]
        public string? OsManufacturer { get; set; }
		
		[JsonPropertyName("osSerial")]
        public string? OsSerial { get; set; }
		
		[JsonPropertyName("disks")]
        public List<DiskItem> Disks { get; set; }
    }
}
