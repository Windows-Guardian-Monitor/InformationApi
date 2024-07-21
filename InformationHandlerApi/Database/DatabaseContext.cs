using InformationHandlerApi.Database.Models;
using Microsoft.EntityFrameworkCore;
namespace InformationHandlerApi.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbWindowsWorkstation>(buildAction =>
        {
            buildAction.HasMany<DbDiskInfo>();
        });
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<DbWindowsWorkstation> Workstations { get; set; }
}
