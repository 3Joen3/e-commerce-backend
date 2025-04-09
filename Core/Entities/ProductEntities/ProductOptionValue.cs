using Core.Entities.AbstractEntities;
using Core.Utils;

namespace Core.Entities.ProductEntities;

public class ProductOptionValue : BaseEntity
{
    public string Value { get; private set; }

    public Guid ProductOptionId { get; private set; }

    public ProductOptionValue(string value)
    {
        Guard.AgainstNullOrWhiteSpace(value, nameof(value));
        Value = value.Trim();
    }

    private ProductOptionValue() { Value = null!; }
}