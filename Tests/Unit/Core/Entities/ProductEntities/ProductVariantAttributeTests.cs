using Core.Entities.ProductEntities;

namespace Tests.Unit.Core.Entities.ProductEntities;

public class ProductVariantAttributeTests
{
    private readonly static string ValidTitle = "Color";
    private readonly static string ValidValue = "Black";

    [Fact]
    public void CreateProductVariantAttribute_WithFullValidData_ShouldSucceed()
    {
        var attribute = new ProductVariantAttribute(ValidTitle, ValidValue);

        Assert.Equal(ValidTitle, attribute.Title);
        Assert.Equal(ValidValue, attribute.Value);
    }

    [Fact]
    public void CreateProductVariantAttribute_WithUntrimmedTitleAndValue_ShouldTrim()
    {
        var untrimmedTitle = " Color ";
        var untrimmedValue = "Black    ";

        var attribute = new ProductVariantAttribute(untrimmedTitle, untrimmedValue);

        Assert.Equal(ValidTitle, attribute.Title);
        Assert.Equal(ValidValue, attribute.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void CreateProductVariantAttribute_WithWhiteSpaceTitle_ShouldThrow(string invalidTitle)
    {
        var ex = Assert.Throws<ArgumentException>(() => new ProductVariantAttribute(invalidTitle, ValidValue));
        Assert.Equal($"String cannot be null or white space. (Parameter 'title')", ex.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void CreateProductVariantAttribute_WithWhiteSpaceValue_ShouldThrow(string invalidValue)
    {
        var ex = Assert.Throws<ArgumentException>(() => new ProductVariantAttribute(ValidTitle, invalidValue));
        Assert.Equal($"String cannot be null or white space. (Parameter 'value')", ex.Message);
    }
}