using Core.Entities.ProductEntities;

namespace Application.Interfaces;

public interface IProductService
{
    Task<Product?> GetByIdAsync(Guid id);
}