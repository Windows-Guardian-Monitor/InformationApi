using InformationHandlerApi.Database.Models;
using Microsoft.EntityFrameworkCore;
namespace InformationHandlerApi.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<DbWindowsWorkstation> Workstations { get; set; }
}
