﻿using ClientServer.Shared.Contracts.Models;
using System.ComponentModel.DataAnnotations;

namespace ClientServer.Shared.Database.Models
{
	public class DbRamNominalInfo : IRamNominalInfo
	{
		[Key]
		public int RamInfoId { get; set; }
		public string TotalMemory { get; set; }
		public string Description { get; set; }
		public string Manufacturer { get; set; }
		public string Speed { get; set; }
	}
}
