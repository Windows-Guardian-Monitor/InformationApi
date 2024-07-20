using InformationHandlerApi.Contracts.Models;

namespace InformationHandlerApi.Database.Models
{
    public class DbDiskInfo : IDiskInfo
    {
        public int Id { get; set; }
        public string AvailableSize { get; set; }
        public string? DiskName { get; set; }
        public string? DiskType { get; set; }
        public string TotalSize { get; set; }
    }
}
