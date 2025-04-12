using Core.Entities.ProductEntities;

namespace Tests.Unit.Core.Entities.ProductEntities;

public class ProductOptionValueTests
{
    private readonly static string ValidValue = "Blue";

    [Fact]
    public void CreateProductOptionValue_WithFullValidData_ShouldSucceed()
    {
        var optionValue = new ProductOptionValue(ValidValue);

        Assert.Equal(ValidValue, optionValue.Value);
    }

    [Fact]
    public void CreateProductOptionValue_WithUntrimmedValue_ShouldTrim()
    {
        var untrimmedValue = "     Blue          ";
        var optionValue = new ProductOptionValue(untrimmedValue);
        Assert.Equal(ValidValue, optionValue.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void CreateProductOptionValue_WithWhiteSpaceTitle_ShouldThrow(string invalidValue)
    {
        var ex = Assert.Throws<ArgumentException>(() => new ProductOptionValue(invalidValue));
        Assert.Equal($"String cannot be null or white space. (Parameter 'value')", ex.Message);
    }
}