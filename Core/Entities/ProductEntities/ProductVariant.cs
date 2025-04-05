using Core.Entities.AbstractEntities;
using Core.Interfaces.Contracts;
using Core.Utils;

namespace Core.Entities.ProductEntities;

public class ProductVariant : BaseEntity
{
    public decimal Price { get; set; }
    public decimal ComparePrice { get; set; }
    public ICollection<ProductVariantAttribute> Attributes { get; set; } = [];
    public ProductImage? Image { get; set; }

    public Guid ProductId { get; set; }

    private ProductVariant() { }

    private ProductVariant(decimal price, decimal comparePrice, ICollection<ProductVariantAttribute>? attributes = null, ProductImage? image = null)
    {
        Price = price;
        ComparePrice = comparePrice;
        Image = image;
        Attributes = attributes ?? [];
    }

    public static ProductVariant CreateWithoutAttributes(decimal price, decimal comparePrice = 0)
    {
        ValidateVariantInput(price);
        return new ProductVariant(price, comparePrice);
    }

    public static ProductVariant CreateWithAttributes<T>(ICollection<T> attributeSource, decimal price,
    decimal comparePrice = 0, ProductImage? image = null) where T : IVariantAttributeCreate
    {
        ValidateVariantInput(price);
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

    private static void ValidateVariantInput(decimal price) => Guard.AgainstNegative(price, nameof(price));
}