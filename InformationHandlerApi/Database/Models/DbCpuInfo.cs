using InformationHandlerApi.Contracts.Models;
using System.ComponentModel.DataAnnotations;

namespace InformationHandlerApi.Database.Models
{
    public class DbCpuInfo : ICpuInfo
    {
        [Key]
        public int CpuInfoId { get; set; }
        public string Architecture { get; set; }
        public string? Description { get; set; }
        public string CpuManufacturer { get; set; }
        public string? Name { get; set; }
    }
}
