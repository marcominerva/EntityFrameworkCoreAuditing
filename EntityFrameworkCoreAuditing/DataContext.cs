using EntityFrameworkCoreAuditing.Entities;
using EntityFrameworkCoreAuditing.Entities.Common;
using Microsoft.EntityFrameworkCore;
using TinyHelpers.EntityFrameworkCore.Extensions;

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
    {
        _ = modelBuilder.Entity<Audit>(entity =>
        {
            _ = entity.Property(e => e.Values).HasJsonConversion();
        });

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var audits = new List<Audit>();

        foreach (var entry in ChangeTracker.Entries())
        {
            // Dot not audit entities that are not tracked, not changed, or not of type IAuditable
            if (entry.State is EntityState.Detached or EntityState.Unchanged || entry.Entity is not IAuditable)
            {
                continue;
            }

            var audit = new Audit
            {
                Action = entry.State switch
                {
                    EntityState.Added => "INSERT",
                    EntityState.Modified => "UPDATE",
                    EntityState.Deleted => "DELETED",
                    _ => "UNKNOWN"
                },
                EntityId = entry.Properties.Single(p => p.Metadata.IsPrimaryKey()).CurrentValue.ToString(),
                EntityName = entry.Metadata.ClrType.Name,
                Timestamp = DateTime.UtcNow,
                Values = entry.Properties.Select(p => new { p.Metadata.Name, p.CurrentValue }).ToDictionary(i => i.Name, i => i.CurrentValue),
            };

            audits.Add(audit);
        }

        Audits.AddRange(audits);
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}

