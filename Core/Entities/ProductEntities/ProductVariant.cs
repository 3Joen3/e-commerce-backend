using Core.Entities.AbstractEntities;
using Core.Interfaces.Contracts;
using Core.Utils;
using Core.ValueObjects;

namespace Core.Entities.ProductEntities;

public class ProductVariant : BaseEntity
{
    public Price Price { get; private set; }
    public Price? ComparePrice { get; private set; }
    public ICollection<ProductVariantAttribute> Attributes { get; private set; }
    public ProductImage? Image { get; private set; }

    public Guid ProductId { get; private set; }

    private ProductVariant(Price price, Price? comparePrice = null, ICollection<ProductVariantAttribute>? attributes = null, ProductImage? image = null)
    {
        Price = price;
        ComparePrice = comparePrice;
        Image = image;
        Attributes = attributes ?? [];
    }

    private ProductVariant() { Price = null!; Attributes = null!; }

    public static ProductVariant CreateWithoutAttributes(decimal inputPrice, decimal inputComparePrice = 0)
    {
        var (price, comparePrice) = SetupPricing(inputPrice, inputComparePrice);
        return new ProductVariant(price, comparePrice);
    }

    public static ProductVariant CreateWithAttributes<T>(ICollection<T> attributeSource, decimal inputPrice,
    decimal inputComparePrice = 0, ProductImage? image = null) where T : IVariantAttributeCreate
    {
        var (price, comparePrice) = SetupPricing(inputPrice, inputComparePrice);

        Guard.AgainstEmptyCollection(attributeSource, nameof(attributeSource));

        var seenTitles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        var attributes = attributeSource.Select(attr =>
        {
            var title = attr.Title.Trim();
            if (!seenTitles.Add(attr.Title)) throw new ArgumentException($"Collection cannot contain duplicate values. (Value: {attr.Title})", nameof(attributeSource));

            return new ProductVariantAttribute(attr.Title, attr.Value);
        });

        return new ProductVariant(price, comparePrice, [.. attributes], image);
    }

    private static (Price price, Price? comparePrice) SetupPricing(decimal inputPrice, decimal inputComparePrice)
    {
        var price = new Price(inputPrice);
        var comparePrice = inputComparePrice > 0 ? new Price(inputComparePrice) : null;

        return (price, comparePrice);
    }
}