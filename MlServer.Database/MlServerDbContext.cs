using Microsoft.EntityFrameworkCore;
using MlServer.Database.Models;

namespace MlServer.Database;

public class MlServerDbContext : DbContext
{
    public DbSet<ObjectDbInfo> ObjectsTable { get; set; }
    
    public DbSet<TableDbInfo> TablesInfosTable { get; set; }

    public MlServerDbContext(DbContextOptions<MlServerDbContext> options)
        : base(options)
    {
        var res = Database.EnsureCreated();
    }
}