namespace Application.Models;

public class ProductVariantCreateModel
{
    public required decimal Price { get; set; }
    public required decimal? ComparePrice { get; set; }
}