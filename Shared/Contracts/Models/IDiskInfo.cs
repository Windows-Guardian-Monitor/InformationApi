namespace InformationHandlerApi.Contracts.Models
{
    public interface IDiskInfo
    {
        public int Id { get; set; }
        string AvailableSize { get; set; }
        string? DiskName { get; set; }
        string? DiskType { get; set; }
        string TotalSize { get; set; }
    }
}
