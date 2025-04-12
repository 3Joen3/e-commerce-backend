using Core.Entities.AbstractEntities;
using Core.Utils;

namespace Core.Entities.ProductEntities;

public class ProductOption : BaseEntity
{
    public string Title { get; private set; }
    public ICollection<ProductOptionValue> Values { get; private set; }

    public Guid ProductId { get; private set; }

    private ProductOption(string title, ICollection<ProductOptionValue> values)
    {
        Title = title;
        Values = values;
    }

    private ProductOption() { Title = null!; Values = null!; }

    public static ProductOption Create(string title, ICollection<string> values)
    {
        Guard.AgainstNullOrWhiteSpace(title, nameof(title));
        Guard.AgainstEmptyCollection(values, nameof(values));
        Guard.AgainstDuplicateString(values, nameof(values));

        var optionValues = values.Select(value => new ProductOptionValue(value));

        var trimmed = title.Trim();

        return new ProductOption(trimmed, [.. optionValues]);
    }
}