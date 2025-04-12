using Core.Entities.ProductEntities;

namespace Tests.Unit.Core.Entities.ProductEntities;

public class ProductImageTests
{
    private static readonly string ValidUrl = "https://www.example.com/image";
    private static readonly string ValidAltText = "Cool red T-shirt";

    //Child

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
    public void CreateProductImage_WithInvalidUrl_ShouldThrow(string invalidUrl)
    {
        var ex = Assert.Throws<ArgumentException>(() => new ProductImage(invalidUrl, ValidAltText));
        Assert.Equal($"Invalid URL: {invalidUrl}. (Parameter 'url')", ex.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public void CreateProductImage_WithEmptyOrWhiteSpaceUrl_ShouldThrow(string emptyUrl)
    {
        var ex = Assert.Throws<ArgumentException>(() => new ProductImage(emptyUrl, ValidAltText));
        Assert.Equal($"String cannot be null or white space. (Parameter 'url')", ex.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public void CreateProductImage_WithEmptyOrWhiteSpaceAltText_ShouldThrow(string emptyAltText)
    {
        var ex = Assert.Throws<ArgumentException>(() => new ProductImage(ValidUrl, emptyAltText));
        Assert.Equal($"String cannot be null or white space. (Parameter 'altText')", ex.Message);
    }
}