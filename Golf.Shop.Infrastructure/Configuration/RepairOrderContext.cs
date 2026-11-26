using Golf.Shop.Application.Domains;
using Microsoft.EntityFrameworkCore;

namespace Golf.Shop.Infrastructure.Configuration;

public class RepairOrderContext : DbContext
{
    public RepairOrderContext(DbContextOptions<RepairOrderContext> options) : base(options)
    {
    }

    public DbSet<RepairOrder> RepairOrders => Set<RepairOrder>();
    public DbSet<RepairService> Services => Set<RepairService>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RepairOrderConfiguration());
        modelBuilder.ApplyConfiguration(new RepairServiceConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}

