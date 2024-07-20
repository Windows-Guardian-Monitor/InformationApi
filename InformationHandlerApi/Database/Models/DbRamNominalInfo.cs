using InformationHandlerApi.Contracts.Models;

namespace InformationHandlerApi.Database.Models
{
    public class DbRamNominalInfo : IRamNominalInfo
    {
        public string TotalMemory { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public uint Speed { get; set; }
    }
}
