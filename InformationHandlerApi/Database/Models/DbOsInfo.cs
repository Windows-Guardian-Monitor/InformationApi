﻿using InformationHandlerApi.Contracts.Models;
using System.ComponentModel.DataAnnotations;

namespace InformationHandlerApi.Database.Models
{
    public class DbOsInfo : IOsInfo
    {
        [Key]
        public int Id { get; set; }
        public string Architecture { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string OSVersion { get; set; }
        public string SerialNumber { get; set; }
        public string VersionStr { get; set; }
        public string WindowsDirectory { get; set; }
    }
}
