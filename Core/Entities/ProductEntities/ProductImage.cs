using Core.Entities.AbstractEntities;

namespace Core.Entities.ProductEntities;

public class ProductImage : ImageEntity
{
    public Guid ProductId { get; set; }

    public ProductImage(string url, string altText) : base(url, altText) { }

    private ProductImage() { }
}