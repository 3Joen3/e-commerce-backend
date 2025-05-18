using Application.Models;
using Core.Entities.ProductEntities;

namespace Application.Interfaces;

public interface IProductService
{
    Task<Result<Product>> CreateProductAsync(ProductCreateModel createModel);
    Task<Product?> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllAsync();
}