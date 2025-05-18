using System.ComponentModel.DataAnnotations;

namespace AdminApi.Requests;
//Possible to send in nothing as price since it defaults to 0. Fix
public class ProductVariantRequest
{
    [Required, Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal Price { get; set; }
    
    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal? ComparePrice { get; set; }
}   