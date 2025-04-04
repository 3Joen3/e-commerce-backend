using Core.Entities.AbstractEntities;

namespace Core.Entities.ProductEntities;

public class ProductOptionValue : BaseEntity
{
    public required string Value { get; set; }

    public int ProductOptionID { get; set; }
}