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
    public DbSet<ProductVariantPrice> ProductVariantPrices { get; set; }
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

        modelBuilder.Entity<ProductVariant>(builder => 
        {
            builder.HasOne(v => v.Price)
            .WithMany()
            .HasForeignKey(v => v.PriceId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.ComparePrice)
            .WithMany()
            .HasForeignKey(v => v.ComparePriceId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProductVariantPrice>(builder => 
        {
            builder.OwnsOne(p => p.Currency, c => 
            {
                c.Property(x => x.Code)
                .HasColumnName("Currency")
                .IsRequired();
            });

            builder.Property(p => p.Amount).IsRequired();
        });
    }
}
