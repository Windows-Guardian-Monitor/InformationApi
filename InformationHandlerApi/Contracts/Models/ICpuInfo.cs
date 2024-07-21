using System.Runtime.InteropServices;

namespace InformationHandlerApi.Contracts.Models
{
    public interface ICpuInfo
    {
        public int Id { get; set; }
        string Architecture { get; set; }
        string? Description { get; set; }
        string CpuManufacturer { get; set; }
        string? Name { get; set; }
    }
}
