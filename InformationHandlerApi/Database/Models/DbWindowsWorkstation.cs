using InformationHandlerApi.Contracts.Models;

namespace InformationHandlerApi.Database.Models
{
    public class DbWindowsWorkstation : IWindowsWorkstation
    {
        public int Id { get; set; }
        public ICpuInfo CpuInfo { get; set; }
        public IEnumerable<IDiskInfo> DisksInfo { get; set; }
        public IOsInfo OsInfo { get; set; }
        public IRamNominalInfo RamInfo { get; set; }
        public string Uuid { get; set; }
    }
}
