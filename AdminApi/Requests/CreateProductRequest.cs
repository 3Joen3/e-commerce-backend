using System.ComponentModel.DataAnnotations;

namespace AdminApi.Requests;

public class CreateProductRequest
{
    [Required, MinLength(1)]
    public string Title { get; set; } = string.Empty;

    [Required, MinLength(1)]
    public IEnumerable<ProductVariantRequest> Variants { get; set; } = null!;

    public string Description { get; set; } = string.Empty;
    public IEnumerable<IFormFile> Media { get; set; } = [];
}