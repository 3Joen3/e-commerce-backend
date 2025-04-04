using Core.Entities.AbstractEntities;
using Core.ExtensionMethods;

namespace Core.Entities.ProductEntities;

public class Product : BaseEntity
{
    private string _title = string.Empty;
    public required string Title
    {
        get => _title;
        set { _title = value; Slug = value.Sluggify(); }
    }
    public string Slug { get; private set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<ProductImage> Images { get; set; } = [];
    public ICollection<ProductOption> Options { get; set; } = [];
    public required ICollection<ProductVariant> Variants { get; set; }
}