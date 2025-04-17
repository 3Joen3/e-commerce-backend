using Core.Entities.AbstractEntities;
using Core.ExtensionMethods;
using Core.Utils;

namespace Core.Entities.ProductEntities;

public class Product : BaseEntity
{
    public string Title { get; private set; }
    public string Slug { get; private set; }
    public ICollection<ProductVariant> Variants { get; private set; }
    public string Description { get; private set; }
    public ICollection<ProductImage> Images { get; private set; }
    public ICollection<ProductOption> Options { get; private set; }

    private Product(string title, ICollection<ProductVariant> variants, string description = "",
        ICollection<ProductImage>? images = null, ICollection<ProductOption>? options = null)
    {
        Title = title;
        Slug = title.Sluggify();
        Variants = variants;
        Description = description;
        Images = images ?? [];
        Options = options ?? [];
    }

    private Product() { Title = null!; Slug = null!; Variants = null!; Description = null!; Images = null!; Options = null!; }

    public static Product Create(string title, ICollection<ProductVariant> variants, string description = "",
        ICollection<ProductImage>? images = null, ICollection<ProductOption>? options = null)
    {
        Guard.AgainstNullOrWhiteSpace(title, nameof(title));
        Guard.AgainstEmptyCollection(variants, nameof(variants));

        if (options != null && options.Count > 0)
            EnsureAllVariantsMatchOptions(options, variants);

        return new Product(title, variants, description, images, options);
    }

    private static void EnsureAllVariantsMatchOptions(ICollection<ProductOption> options, ICollection<ProductVariant> variants)
    {
        foreach (var variant in variants)
        {
            foreach (var option in options)
            {
                var attribute = FindVariantAttributeByOptionTitle(option.Title, variant.Attributes)
                    ?? throw new ArgumentException($"Product variant is missing required attribute title: {option.Title}");

                if (!IsAttributeValueAllowed(option.Values, attribute))
                    throw new ArgumentException($"Product variant with attribute value '{attribute.Value}' does not match allowed values for option: {option.Title}");
            }
        }
    }

    private static ProductVariantAttribute? FindVariantAttributeByOptionTitle(string optionTitle, ICollection<ProductVariantAttribute> attributes)
        => attributes.FirstOrDefault(attr => attr.Title.Equals(optionTitle, StringComparison.OrdinalIgnoreCase));

    private static bool IsAttributeValueAllowed(ICollection<ProductOptionValue> optionValues, ProductVariantAttribute attribute)
        => optionValues.Any(optValue => optValue.Value.Equals(attribute.Value, StringComparison.OrdinalIgnoreCase));
}