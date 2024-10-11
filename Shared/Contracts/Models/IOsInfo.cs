namespace ClientServer.Shared.Contracts.Models
{
	public interface IOsInfo
	{
		string Architecture { get; set; }
		string Description { get; set; }
		string OsManufacturer { get; set; }
		string OsVersion { get; set; }
		string SerialNumber { get; set; }
		string VersionStr { get; set; }
		string WindowsDirectory { get; set; }
	}
}
