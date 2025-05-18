namespace Application.Models;

public class ProductCreateModel
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required IEnumerable<ProductVariantCreateModel> Variants { get; set; }
    public required IEnumerable<FileUpload> UploadedFiles { get; set; }
}