﻿using InformationHandlerApi.Contracts.Models;

namespace InformationHandlerApi.Database.Models
{
    public class DbOsInfo : IOsInfo
    {
        public string Architecture { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public Version OSVersion { get; set; }
        public string SerialNumber { get; set; }
        public string VersionStr { get; set; }
        public string WindowsDirectory { get; set; }
    }
}
