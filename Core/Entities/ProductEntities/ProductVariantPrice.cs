using Core.Entities.AbstractEntities;
using Core.ValueObjects;

namespace Core.Entities.ProductEntities;

public class ProductVariantPrice : PriceEntity
{
    public Guid ProductVariantId { get; set; }

    public ProductVariantPrice(decimal amount, Currency currency) : base(amount, currency) {}
    private ProductVariantPrice() {}
}