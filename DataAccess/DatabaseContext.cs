namespace DataAccess;

using Common.Entities;
using Common.Mappers;
using Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options) 
    { 
    }

    public virtual DbSet<Driver> Drivers { get; set; }
    public virtual DbSet<Delivery> Deliveries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DriverMap());
        modelBuilder.ApplyConfiguration(new DeliveryMap());
    }

    protected override void OnConfiguring
        (DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseInMemoryDatabase(databaseName: "LogisticDb");
}
