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

        public string? HostName { get; set; }
        public string? Uuid { get; set; }
		public string? CpuDescription { get; set; }
        public string? CpuName { get; set; }
        public string? CpuManufacturer { get; set; }
        public string? CpuArchitecture { get; set; }
        public string? TotalMemory { get; set; }
        public string? MemorySpeed { get; set; }
        public string? MemoryManufacturer { get; set; }
        public string? OSDescription { get; set; }
        public string? OsManufacturer { get; set; }
        public string? OsSerial { get; set; }
        public List<DiskItem> Disks { get; set; }
    }
}
