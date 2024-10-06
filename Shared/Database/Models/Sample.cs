namespace InformationHandlerApi.Database.Models
{

    public class Rootobject
    {
        public int Id { get; set; }
        public Cpuinfo CpuInfo { get; set; }
        public Disksinfo[] DisksInfo { get; set; }
        public Osinfo OsInfo { get; set; }
        public Raminfo RamInfo { get; set; }
        public object Uuid { get; set; }
    }

    public class Cpuinfo
    {
        public int Id { get; set; }
        public string Architecture { get; set; }
        public string Description { get; set; }
        public string manufacturer { get; set; }
        public string Name { get; set; }
    }

    public class Osinfo
    {
        public int Id { get; set; }
        public string Architecture { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string OsVersion { get; set; }
        public string SerialNumber { get; set; }
        public string VersionStr { get; set; }
        public string WindowsDirectory { get; set; }
    }

    public class Raminfo
    {
        public int Id { get; set; }
        public string TotalMemory { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Speed { get; set; }
    }

    public class Disksinfo
    {
        public int Id { get; set; }
        public string AvailableSize { get; set; }
        public string DiskName { get; set; }
        public string DiskType { get; set; }
        public string TotalSize { get; set; }
    }

}
