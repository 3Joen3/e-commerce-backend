using Core.Entities.AbstractEntities;

namespace Core.Entities.ProductEntities;

public class ProductVariant : BaseEntity
{
    public decimal Price { get; set; }
    public decimal ComparePrice { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public ICollection<ProductVariantAttribute> Attributes { get; set; } = [];

    public Guid ProductId { get; set; }

    private ProductVariant() { }

    private ProductVariant(decimal price, decimal comparePrice, string imageUrl = "", ICollection<ProductVariantAttribute>? attributes = null)
    {
        if (price < 0) throw new ArgumentException("Price cannot be negative.");

        Price = price;
        ComparePrice = comparePrice;
        ImageUrl = imageUrl;
        Attributes = attributes ?? [];
    }

    public static ProductVariant CreateWithoutAttributes(decimal price, decimal comparePrice = 0) => new(price, comparePrice);
}