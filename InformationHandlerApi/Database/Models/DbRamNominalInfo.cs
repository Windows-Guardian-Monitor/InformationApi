using InformationHandlerApi.Contracts.Models;
using System.ComponentModel.DataAnnotations;

namespace InformationHandlerApi.Database.Models
{
    public class DbRamNominalInfo : IRamNominalInfo
    {
        [Key]
        public int Id { get; set; }
        public string TotalMemory { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Speed { get; set; }
    }
}
