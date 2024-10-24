﻿using ClientServer.Shared.Contracts.Models;
using System.ComponentModel.DataAnnotations;

namespace ClientServer.Shared.Database.Models
{
	public class DbCpuInfo : ICpuInfo
	{
		[Key]
		public int CpuInfoId { get; set; }
		public string Architecture { get; set; }
		public string? Description { get; set; }
		public string CpuManufacturer { get; set; }
		public string? Name { get; set; }

		//public int WorkstationId { get; set; }
		//public DbWindowsWorkstation Workstation { get; set; } = null!;
	}
}