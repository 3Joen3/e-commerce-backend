using Core.Entities.ProductEntities;

namespace Core.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product> AddAsync(Product product);
    Task<Product?> GetByIdAsync(Guid id);
}