using System.Collections.ObjectModel;
using Core.Entities.ProductEntities;

namespace Tests.Unit.Core.Entities.ProductEntities;

public class ProductTests
{
    private static readonly string ValidTitle = "Some Cool Item";
    private static readonly ICollection<ProductVariant> ValidVariantsWithoutAttributes = [ProductVariant.CreateWithoutAttributes(100)];

    [Fact]
    public void CreateProduct_WithFullValidData_ShouldSucceed()
    {
        var description = "This is a product description!";

        var images = new Collection<ProductImage>()
        {
             new("https://www.example.com/image", "Cool Black T-shirt")
        };

        var options = new Collection<ProductOption>()
        {
            ProductOption.Create("Size", ["S", "M", "L"]),
            ProductOption.Create("Color", ["White", "Black"]),
        };

        var sizes = new Collection<TestAttributeInput>()
        {
            new() { Title = "Size", Value = "S" },
            new() { Title = "Size", Value = "M" },
            new() { Title = "Size", Value = "L" }
        };

        var colors = new Collection<TestAttributeInput>()
        {
            new() { Title = "Color", Value = "White" },
            new() { Title = "Color", Value = "Black" }
        };

        var variants = new Collection<ProductVariant>()
        {
            ProductVariant.CreateWithAttributes([sizes[0], colors[0]], 199),
            ProductVariant.CreateWithAttributes([sizes[0], colors[1]], 199),
            ProductVariant.CreateWithAttributes([sizes[1], colors[0]], 199),
            ProductVariant.CreateWithAttributes([sizes[1], colors[1]], 199),
            ProductVariant.CreateWithAttributes([sizes[2], colors[0]], 199),
            ProductVariant.CreateWithAttributes([sizes[2], colors[1]], 199),
        };

        var product = Product.Create(ValidTitle, variants, description, images, options);

        Assert.Equal(ValidTitle, product.Title);
        Assert.Equal("some-cool-item", product.Slug);
        Assert.Equal(description, product.Description);
        Assert.Equal(images, product.Images);
        Assert.Equal(options, product.Options);
        Assert.Equal(variants, product.Variants);
    }

    [Fact]
    public void CreateProduct_WithOnlyRequiredData_ShouldSetDefaults()
    {
        var product = Product.Create(ValidTitle, ValidVariantsWithoutAttributes);

        Assert.Equal(ValidTitle, product.Title);
        Assert.Equal("some-cool-item", product.Slug);
        Assert.Equal("", product.Description);
        Assert.Empty(product.Images);
        Assert.Empty(product.Options);
    }

    [Fact]
    public void CreateProduct_WithNullImagesOrOptions_ShouldDefaultToEmpty()
    {
        var product = Product.Create(ValidTitle, ValidVariantsWithoutAttributes, images: null, options: null);

        Assert.Empty(product.Images);
        Assert.Empty(product.Options);
    }

    [Theory]
    [InlineData("Märke Hoodie för män", "marke-hoodie-for-man")]
    [InlineData("adidas Originals | CAMPUS 00s - Låga sneakers", "adidas-originals-campus-00s-laga-sneakers")]
    [InlineData("Rogue St Max 24 - Stål/Grafit", "rogue-st-max-24-stal-grafit")]
    public void SettingTitle_ShouldSetSlugCorrectly(string title, string expectedSlug)
    {
        var product = Product.Create(title, ValidVariantsWithoutAttributes);

        Assert.Equal(expectedSlug, product.Slug);
    }

    [Fact]
    public void CreateProduct_WithEmptyOrWhiteSpaceTitle_ShouldThrow()
    {
        var title = "";

        var ex = Assert.Throws<ArgumentException>(() =>
        {
            Product.Create(title, ValidVariantsWithoutAttributes);
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

    [Fact]
    public void CreateProduct_WithMismatchingAttributeValue_ShouldThrow()
    {
        var options = new Collection<ProductOption>()
        {
            ProductOption.Create("Size", ["S", "M"])
        };

        var matchingSize = new TestAttributeInput() { Title = "Size", Value = "S" };
        var missMatchingSize = new TestAttributeInput() { Title = "Size", Value = "L" };

        var variants = new Collection<ProductVariant>()
        {
            ProductVariant.CreateWithAttributes([matchingSize], 99),
            ProductVariant.CreateWithAttributes([missMatchingSize], 99),
        };

        var expectedAttributeValue = variants[1].Attributes.First().Value;
        var expectedOptionTitle = options.First().Title;

        var ex = Assert.Throws<ArgumentException>(() => Product.Create("T-shirt", variants, options: options));
        Assert.Equal($"Product variant with attribute value '{expectedAttributeValue}' does not match allowed values for option: {expectedOptionTitle}", ex.Message);
    }

    [Fact]
    public void CreateProduct_WithMismatchingAttributeTitle_ShouldThrow()
    {
        var options = new Collection<ProductOption>()
        {
            ProductOption.Create("Size", [ "S", "M" ])
        };

        var mismatchingTitleAttribute = new TestAttributeInput() { Title = "Color", Value = "Red" };

        var variants = new Collection<ProductVariant>()
        {
            ProductVariant.CreateWithAttributes( [ mismatchingTitleAttribute ] , 99)
        };

        var ex = Assert.Throws<ArgumentException>(() => Product.Create(ValidTitle, variants, options: options));
        Assert.Equal($"Product variant is missing required attribute title: {options[0].Title}", ex.Message);
    }
}