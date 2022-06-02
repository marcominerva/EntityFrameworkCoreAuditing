using EntityFrameworkCoreAuditing.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreAuditing;

public class DataContext : DbContext
{
    private const string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=Northwind60;Integrated Security=True";

    public DbSet<Audit> Audits { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _ = optionsBuilder.UseSqlServer(connectionString);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => base.OnModelCreating(modelBuilder);
}

