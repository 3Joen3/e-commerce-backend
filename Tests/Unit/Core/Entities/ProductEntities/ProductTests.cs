using System.Collections.ObjectModel;
using Core.Entities.ProductEntities;

namespace Tests.Unit.Core.Entities.ProductEntities;

public class ProductTests
{
    private static readonly string ValidTitle = "Some Cool Item";
    private static readonly ICollection<ProductVariant> ValidVariants = [ProductVariant.CreateWithoutAttributes(100)];

    [Theory]
    [InlineData("Märke Hoodie för män", "marke-hoodie-for-man")]
    [InlineData("adidas Originals | CAMPUS 00s - Låga sneakers", "adidas-originals-campus-00s-laga-sneakers")]
    [InlineData("Rogue St Max 24 - Stål/Grafit", "rogue-st-max-24-stal-grafit")]
    public void SettingTitle_ShouldSetSlugCorrectly(string title, string expectedSlug)
    {
        var product = Product.Create(title, ValidVariants);

        Assert.Equal(expectedSlug, product.Slug);
    }

    [Fact]
    public void CreateProduct_WithFullValidData_ShouldSucceed()
    {
        var description = "This is a product description!";
        var images = new Collection<ProductImage>() { new("https://www.example.com/image", "Cool Black T-shirt") };
        var options = new Collection<ProductOption>() { ProductOption.Create("Size", ["S", "M", "L"]) };

        var product = Product.Create(ValidTitle, ValidVariants, description, images, options);

        Assert.Equal(ValidTitle, product.Title);
        Assert.Equal("some-cool-item", product.Slug);
        Assert.Equal(description, product.Description);
        Assert.Equal(images, product.Images);
        Assert.Equal(options, product.Options);
        Assert.Equal(ValidVariants, product.Variants);
    }

    [Fact]
    public void CreateProduct_WithOnlyRequiredData_ShouldSetDefaults()
    {
        var product = Product.Create(ValidTitle, ValidVariants);

        Assert.Equal(ValidTitle, product.Title);
        Assert.Equal("some-cool-item", product.Slug);
        Assert.Equal("", product.Description);
        Assert.Empty(product.Images);
        Assert.Empty(product.Options);
    }

    [Fact]
    public void CreateProduct_WithNullImagesOrOptions_ShouldDefaultToEmpty()
    {
        var product = Product.Create(ValidTitle, ValidVariants, images: null, options: null);

        Assert.Empty(product.Images);
        Assert.Empty(product.Options);
    }

    [Fact]
    public void CreateProduct_WithEmptyOrWhiteSpaceTitle_ShouldThrow()
    {
        var title = "";

        var ex = Assert.Throws<ArgumentException>(() =>
        {
            Product.Create(title, ValidVariants);
        });

        Assert.Equal("String cannot be null or white space. (Parameter 'title')", ex.Message);
    }

    [Fact]
    public void CreateProduct_WithZeroVariants_ShouldThrow()
    {
        var emptyVariants = new Collection<ProductVariant>();

        var ex = Assert.Throws<ArgumentException>(() =>
        {
            Product.Create(ValidTitle, emptyVariants);
        });

        Assert.Equal("Collection must contain at least 1 item(s). (Parameter 'variants')", ex.Message);
    }
}