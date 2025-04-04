using Core.Entities.AbstractEntities;

namespace Core.Entities.ProductEntities;

public class ProductOption : BaseEntity
{
    public required string Title { get; set; }
    public required ICollection<ProductOptionValue> Values { get; set; } = [];

    public int ProductID { get; set; }
}