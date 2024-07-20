namespace InformationHandlerApi.Contracts.Models
{
    public interface IRamNominalInfo
    {
        string TotalMemory { get; set; }
        string Description { get; set; }
        string Manufacturer { get; set; }
        uint Speed { get; set; }
    }
}
