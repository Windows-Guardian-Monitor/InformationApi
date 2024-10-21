using ClientServer.Shared.Contracts.Models;
using System.ComponentModel.DataAnnotations;

namespace ClientServer.Shared.Database.Models
{
	public class DbWindowsWorkstation : IWindowsWorkstation
	{
		[Key]
		public int Id { get; set; }
		public string Uuid { get; set; }
		//public int CpuInfoId { get; set; }
		public DbCpuInfo CpuInfo { get; set; }
		public IEnumerable<DbDiskInfo> DisksInfo { get; set; }
		public DbOsInfo OsInfo { get; set; }
		public DbRamNominalInfo RamInfo { get; set; }
		public string HostName { get; set; }
	}
}