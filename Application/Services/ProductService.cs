    using Application.Interfaces;
    using Core.Entities.ProductEntities;
    using Core.Interfaces.Repositories;

    namespace Application.Services;

    public class ProductService(IProductRepository productRepository) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<Product?> GetByIdAsync(Guid id) => await _productRepository.GetByIdAsync(id);
    }