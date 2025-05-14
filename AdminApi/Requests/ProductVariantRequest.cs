using System.ComponentModel.DataAnnotations;

namespace AdminApi.Requests;

public class ProductVariantRequest
{
    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal Price { get; set; }
    
    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal ComparePrice { get; set; }
}