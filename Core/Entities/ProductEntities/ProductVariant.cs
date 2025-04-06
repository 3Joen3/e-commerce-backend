using Core.Entities.AbstractEntities;
using Core.Interfaces.Contracts;
using Core.Utils;
using Core.ValueObjects;

namespace Core.Entities.ProductEntities;

public class ProductVariant : BaseEntity
{
    public Price Price { get; set; }
    public Price? ComparePrice { get; set; }
    public ICollection<ProductVariantAttribute> Attributes { get; set; } = [];
    public ProductImage? Image { get; set; }

    public Guid ProductId { get; set; }

    private ProductVariant() { Price = null!; }

    private ProductVariant(Price price, Price? comparePrice = null, ICollection<ProductVariantAttribute>? attributes = null, ProductImage? image = null)
    {
        Price = price;
        ComparePrice = comparePrice;
        Image = image;
        Attributes = attributes ?? [];
    }

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
            Guard.AgainstNullOrWhiteSpace(attr.Title, nameof(attr.Title));
            Guard.AgainstNullOrWhiteSpace(attr.Value, nameof(attr.Value));

            var trimmedTitle = attr.Title.Trim();

            if (!seenTitles.Add(trimmedTitle)) throw new ArgumentException($"Collection cannot contain duplicate values. (Value: {trimmedTitle})", nameof(attributeSource));

            return new ProductVariantAttribute
            {
                Title = trimmedTitle,
                Value = attr.Value.Trim()
            };
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