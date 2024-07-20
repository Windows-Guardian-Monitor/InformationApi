﻿using System.Runtime.InteropServices;

namespace InformationHandlerApi.Contracts.Models
{
    public interface ICpuInfo
    {
        public int Id { get; set; }
        Architecture Architecture { get; set; }
        string? Description { get; set; }
        string Manufacturer { get; set; }
        string? Name { get; set; }
    }
}
