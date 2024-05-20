using Microsoft.EntityFrameworkCore;
using RedisCacheExample.Entities;

namespace RedisCacheExample;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Use snake_case naming convention for PostgreSQL
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");

            entity.Property(e => e.Id)
                  .HasColumnName("id");

            entity.Property(e => e.Name)
                  .HasColumnName("name");

            entity.Property(e => e.Price)
                  .HasColumnName("price");
        });
    }
}
