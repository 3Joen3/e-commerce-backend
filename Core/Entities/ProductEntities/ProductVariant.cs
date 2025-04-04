using Core.Entities.AbstractEntities;

namespace Core.Entities.ProductEntities;

public class ProductVariant : BaseEntity
{
    public required decimal Price { get; set; }
    public decimal ComparePrice { get; set; }
    public decimal Cost { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public required ICollection<ProductVariantAttribute> Attributes { get; set; }

    public Guid ProductId { get; set; }
}