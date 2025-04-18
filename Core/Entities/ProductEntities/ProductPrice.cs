using Core.Entities.AbstractEntities;
using Core.ValueObjects;

namespace Core.Entities.ProductEntities;

public class ProductPrice : PriceEntity
{
    public Guid ProductVariantId { get; set; }

    public ProductPrice(decimal amount, Currency currency) : base(amount, currency) {}
    private ProductPrice() {}
}