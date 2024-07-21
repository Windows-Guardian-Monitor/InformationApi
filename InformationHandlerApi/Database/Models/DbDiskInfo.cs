using InformationHandlerApi.Contracts.Models;
using System.ComponentModel.DataAnnotations;

namespace InformationHandlerApi.Database.Models
{
    public class DbDiskInfo : IDiskInfo
    {
        [Key]
        public int Id { get; set; }
        public string AvailableSize { get; set; }
        public string? DiskName { get; set; }
        public string? DiskType { get; set; }
        public string TotalSize { get; set; }
    }
}
