using Core.Entities.AbstractEntities;
using Core.Interfaces.Contracts;
using Core.Utils;
using Core.ValueObjects;

namespace Core.Entities.ProductEntities;

public class ProductVariant : BaseEntity
{
    public ProductVariantPrice Price { get; private set; }
    public Guid PriceId { get; set; }

    public ProductVariantPrice? ComparePrice { get; private set; }
    public Guid ComparePriceId { get; set; }

    public ICollection<ProductVariantAttribute> Attributes { get; private set; }
    public ProductImage? Image { get; private set; }

    public Guid ProductId { get; private set; }

    private ProductVariant(ProductVariantPrice price, ProductVariantPrice? comparePrice = null, ICollection<ProductVariantAttribute>? attributes = null, ProductImage? image = null)
    {
        Price = price;
        ComparePrice = comparePrice;
        Image = image;
        Attributes = attributes ?? [];
    }

    private ProductVariant() { Price = null!; Attributes = null!; }

    public static ProductVariant CreateWithoutAttributes(decimal inputPrice, decimal? inputComparePrice = null)
    {
        var (price, comparePrice) = SetupPricing(inputPrice, inputComparePrice);
        return new ProductVariant(price, comparePrice);
    }

    public static ProductVariant CreateWithAttributes<T>(ICollection<T> attributeSource, decimal inputPrice,
    decimal? inputComparePrice = null, ProductImage? image = null) where T : IVariantAttributeCreate
    {
        var (price, comparePrice) = SetupPricing(inputPrice, inputComparePrice);

        Guard.AgainstEmptyCollection(attributeSource, nameof(attributeSource));
        Guard.AgainstDuplicateString(attributeSource.Select((a) => a.Title), nameof(attributeSource));

        var attributes = attributeSource.Select(attr => new ProductVariantAttribute(attr.Title, attr.Value));

        return new ProductVariant(price, comparePrice, [.. attributes], image);
    }

    private static (ProductVariantPrice price, ProductVariantPrice? comparePrice) SetupPricing(decimal inputPrice, decimal? inputComparePrice)
    {
        if (inputComparePrice.HasValue) Guard.AgainstLowerValue(inputComparePrice.Value, inputPrice, nameof(inputComparePrice));

        var price = new ProductVariantPrice(inputPrice, Currency.SEK);
        var comparePrice = inputComparePrice.HasValue ? new ProductVariantPrice(inputComparePrice.Value, Currency.SEK) : null;

        return (price, comparePrice);
    }
}