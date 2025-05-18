using Application.Models;

namespace AdminApi.Requests;

public static class RequestMapping
{
    public static ProductCreateModel ToProductCreateModel(this CreateProductRequest request)
    {
        return new ProductCreateModel()
        {
            Title = request.Title,
            Description = request.Description,
            UploadedFiles = request.Media.Select(MapFile),
            Variants = request.Variants.Select(v => v.ToProductVariantCreateModel())
        };
    }

    private static FileUpload MapFile(IFormFile file) => new() { Name = file.FileName, Stream = file.OpenReadStream() };

    public static ProductVariantCreateModel ToProductVariantCreateModel(this ProductVariantRequest request)
    {
        return new ProductVariantCreateModel()
        {
            Price = request.Price,
            ComparePrice = request.ComparePrice
        };
    }
}