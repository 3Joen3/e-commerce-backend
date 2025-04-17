using Core.Entities.ProductEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductOption> ProductOptions { get; set; }
    public DbSet<ProductOptionValue> ProductOptionValues { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }
    public DbSet<ProductVariantAttribute> ProductVariantAttributes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductImage>(builder =>
        {
            builder.OwnsOne(i => i.Url, url =>
            {
                url.Property(u => u.Url)
                .HasColumnName("Url")
                .IsRequired();
            });

            builder.Property(p => p.AltText).IsRequired();
        });
    }
}
