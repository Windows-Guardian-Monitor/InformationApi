using ClientServer.Shared.Contracts.Models;
using System.ComponentModel.DataAnnotations;

namespace ClientServer.Shared.Database.Models
{
	public class DbDiskInfo : IDiskInfo
	{
		[Key]
		public int Id { get; set; }
		public string AvailableSize { get; set; }
		public string? DiskName { get; set; }
		public string? DiskType { get; set; }
		public string TotalSize { get; set; }

		public int WorkstationId { get; set; }
		public DbWindowsWorkstation Workstation { get; set; }
	}
}
