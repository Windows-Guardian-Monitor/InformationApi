using InformationHandlerApi.Contracts.Models;
using System.Runtime.InteropServices;

namespace InformationHandlerApi.Database.Models
{
    public class CpuInfo : ICpuInfo
    {
        public int Id { get; set; }
        public Architecture Architecture { get; set; }
        public string? Description { get; set; }
        public string Manufacturer { get; set; }
        public string? Name { get; set; }
    }
}
