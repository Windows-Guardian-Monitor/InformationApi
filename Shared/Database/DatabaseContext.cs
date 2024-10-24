﻿using ClientServer.Shared.Database.Models;
using ClientServer.Shared.Database.Models.Authentication;
using ClientServer.Shared.Models;
using ClientServer.Shared.Requests.Events;
using Microsoft.EntityFrameworkCore;
using static ClientServer.Shared.Models.ProgramWithTime;
namespace ClientServer.Shared.Database;

public class DatabaseContext : DbContext
{
	//DO NOT DELETE THIS CTOR
	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		//modelBuilder.Entity<DbRule>()
		//    .HasMany(r => r.Programs)
		//    .WithMany(p => p.Rules)
		//    .UsingEntity<Dictionary<string, object>>(
		//    "RuleProgram",
		//    j => j.HasOne<DbRuleProgram>().WithMany().HasForeignKey("ProgramId"),
		//    j => j.HasOne<DbRule>().WithMany().HasForeignKey("RuleId"));

		modelBuilder.Entity<DbRule>(r => r.HasMany(r => r.Programs).WithOne(p => p.ForeignRule));
	}

	public DbSet<DbWindowsWorkstation> Workstations { get; set; }
	public DbSet<DbDiskInfo> Disks { get; set; }
	public DbSet<DbCpuInfo> Cpus { get; set; }
	public DbSet<DbOsInfo> Systems { get; set; }
	public DbSet<DbRamNominalInfo> Rams { get; set; }
	public DbSet<DbProgram> Programs { get; set; }
	public DbSet<DbRule> Rules { get; set; }
    public DbSet<DbWorkstationSpecificRule> WsRules { get; set; }
    public DbSet<DbUser> Users { get; set; }
	public DbSet<ProcessFinishedEvent> ProcessFinishedEvents { get; set; }
    public DbSet<CpuPerformanceModel> CpuPerformanceMonitor { get; set; }
    public DbSet<RamPerformanceModel> RamPerformanceMonitor { get; set; }

    public DbSet<DbProgramWithExecutionTime> ExecutionTimes { get; set; }
}
