using InformationHandlerApi.Database.Models;
using Microsoft.EntityFrameworkCore;
namespace InformationHandlerApi.Database;

public class DatabaseContext : DbContext
{
    //DO NOT DELETE THIS CTOR
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbRule>(buildAction =>
        {
            buildAction.HasMany(r => r.Programs);
        });

        //modelBuilder.Entity<DbWindowsWorkstation>()
        //    .HasOne(x => x.CpuInfo).WithOne(x => x.Workstation).HasForeignKey<DbCpuInfo>(x => x.WorkstationId).IsRequired();
    }

    public DbSet<DbWindowsWorkstation> Workstations { get; set; }
    public DbSet<DbDiskInfo> Disks { get; set; }
    public DbSet<DbCpuInfo> Cpus { get; set; }
    public DbSet<DbOsInfo> Systems { get; set; }
    public DbSet<DbRamNominalInfo> Rams { get; set; }
    public DbSet<DbProgram> Programs { get; set; }
    public DbSet<DbRule> Rules { get; set; }
}
