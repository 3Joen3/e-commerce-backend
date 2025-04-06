using Core.Entities.AbstractEntities;
using Core.Utils;

namespace Core.Entities.ProductEntities;

public class ProductVariantAttribute : BaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public string Value { get; private set; } = string.Empty;


    public Guid ProductVariantId { get; set; }

    public ProductVariantAttribute(string title, string value)
    {
        Guard.AgainstNullOrWhiteSpace(title, nameof(title));
        Guard.AgainstNullOrWhiteSpace(value, nameof(value));

        Title = title.Trim();
        Value = value.Trim();
    }
    private ProductVariantAttribute() { }
}