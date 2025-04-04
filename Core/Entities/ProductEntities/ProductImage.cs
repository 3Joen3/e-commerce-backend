using Core.Entities.AbstractEntities;

namespace Core.Entities.ProductEntities;

public class ProductImage : ImageEntity
{
    public Guid ProductId { get; set; }
}