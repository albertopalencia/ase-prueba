using ReservarTurnos.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ReservarTurnos.Infraestructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Trade> Trades { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Shift> Shifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
