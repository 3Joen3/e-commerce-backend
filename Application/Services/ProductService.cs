using Application.Interfaces;
using Application.Models;
using Core.Entities.ProductEntities;
using Core.Interfaces.Repositories;

namespace Application.Services;

public class ProductService(IProductRepository productRepository, IFileUploader fileUploader) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IFileUploader _fileUploader = fileUploader;

    public async Task<Result<Product>> CreateProductAsync(ProductCreateModel createModel)
    {
        try
        {
            var productVariants = HandleVariants(createModel);

            if (productVariants.Count < 1) return Result<Product>.Fail("Can't create product without ProductVariants.");

            var media = await HandleFiles([.. createModel.UploadedFiles]);

            var product = Product.Create(createModel.Title, productVariants, createModel.Description, media);
            product = await _productRepository.AddAsync(product);

            return Result<Product>.Ok(product);
        }
        catch (Exception ex)
        {
            return Result<Product>.Fail(ex.Message);
        }
    }

    private static List<ProductVariant> HandleVariants(ProductCreateModel createModel)
    {
        var variants = createModel.Variants.ToList();

        if (variants.Count == 1)
        {
            var temp = createModel.Variants.First();
            var variant = ProductVariant.CreateWithoutAttributes(temp.Price, temp.ComparePrice);
            return [variant];
        }
        else
        {
            return [];
        }
    }

    private async Task<ICollection<ProductImage>> HandleFiles(List<FileUpload> uploads)
    {
        if (uploads.Count == 0) return [];

        var productImages = new List<ProductImage>();

        foreach (var file in uploads)
        {
            var url = await _fileUploader.Upload(file);
            var image = new ProductImage(url, file.Name);
            productImages.Add(image);
        }

        return productImages;
    }

    public async Task<Product?> GetByIdAsync(Guid id) => await _productRepository.GetByIdAsync(id);

    public async Task<IEnumerable<Product>> GetAllAsync() => await _productRepository.GetAllAsync();
}