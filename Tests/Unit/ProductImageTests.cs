using Core.Entities.ProductEntities;

namespace Tests.Unit;

public class ProductImageTests
{
    private static readonly string ValidUrl = "https://www.example.com/image";
    private static readonly string ValidAltText = "Cool red T-shirt";

    [Fact]
    public void CreateProductImage_WithUrlAndAltText_ShouldSucceed()
    {
        var image = new ProductImage(ValidUrl, ValidAltText);

        Assert.Equal(ValidUrl, image.Url.ToString());
        Assert.Equal(ValidAltText, image.AltText);
    }

    [Theory]
    [InlineData("Something random.")]
    [InlineData("Google.com")]
    public void CreateProductImage_WithInvalidUrl_ShouldThrow(string url)
    {
        var ex = Assert.Throws<ArgumentException>(() => new ProductImage(url, ValidAltText));
        Assert.Equal($"Invalid URL: {url}. (Parameter 'url')", ex.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public void CreateProductImage_WithEmptyOrWhiteSpaceUrl_ShouldThrow(string url)
    {
        var ex = Assert.Throws<ArgumentException>(() => new ProductImage(url, ValidAltText));
        Assert.Equal($"String cannot be null or white space. (Parameter 'url')", ex.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public void CreateProductImage_WithEmptyOrWhiteSpaceAltText_ShouldThrow(string altText)
    {
        var ex = Assert.Throws<ArgumentException>(() => new ProductImage(ValidUrl, altText));
        Assert.Equal($"String cannot be null or white space. (Parameter 'altText')", ex.Message);
    }
}