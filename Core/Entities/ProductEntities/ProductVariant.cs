using Core.Entities.AbstractEntities;
using Core.Utils;

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
        Guard.AgainstNegative(price, nameof(price));

        Price = price;
        ComparePrice = comparePrice;
        ImageUrl = imageUrl;
        Attributes = attributes ?? [];
    }

    public static ProductVariant CreateWithoutAttributes(decimal price, decimal comparePrice = 0) => new(price, comparePrice);
}