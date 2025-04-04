using Core.Entities.AbstractEntities;

namespace Core.Entities.ProductEntities;

public class ProductImage : ImageEntity
{
    public int ProductID { get; set; }
}