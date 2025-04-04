using Core.Entities.AbstractEntities;

namespace Core.Entities.ProductEntities;

public class ProductVariantAttribute : BaseEntity
{
    public required string Title { get; set; }
    public required string Value { get; set; }

    public Guid ProductVariantId { get; set; }
}