using ClientServer.Shared.Contracts.Models;
using System.ComponentModel.DataAnnotations;

namespace ClientServer.Shared.Database.Models
{
	public class DbOsInfo : IOsInfo
	{
		[Key]
		public int OsInfoId { get; set; }
		public string Architecture { get; set; }
		public string Description { get; set; }
		public string OsManufacturer { get; set; }
		public string OsVersion { get; set; }
		public string SerialNumber { get; set; }
		public string VersionStr { get; set; }
		public string WindowsDirectory { get; set; }
	}
}
