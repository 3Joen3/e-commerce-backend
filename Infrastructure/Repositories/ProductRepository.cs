using Core.Entities.ProductEntities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Product?> GetByIdAsync(Guid id) => await _context.Products.FindAsync(id);
}                                                                                                                                                                   