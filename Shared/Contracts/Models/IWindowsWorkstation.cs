using InformationHandlerApi.Database.Models;

namespace InformationHandlerApi.Contracts.Models
{
    public interface IWindowsWorkstation
    {
        int Id { get; set; }
        DbCpuInfo CpuInfo { get; set; }
        IEnumerable<DbDiskInfo> DisksInfo { get; set; }
        DbOsInfo OsInfo { get; set; }
        DbRamNominalInfo RamInfo { get; set; }
        string Uuid { get; set; }
        string HostName { get; set; }
    }
}
