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

    public static Product Create(string title, ICollection<ProductVariant> variants, string description = "",
        ICollection<ProductImage>? images = null, ICollection<ProductOption>? options = null)
    {
        Guard.AgainstNullOrWhiteSpace(title, nameof(title));
        Guard.AgainstEmptyCollection(variants, nameof(variants));

        return new Product(title, variants, description, images, options);
    }
}