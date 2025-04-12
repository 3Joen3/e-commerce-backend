using System.Collections.ObjectModel;
using Core.Entities.ProductEntities;

namespace Tests.Unit.Core.Entities.ProductEntities;

public class ProductOptionTests
{
    private static readonly string ValidTitle = "Size";
    private static readonly ICollection<string> ValidValues = ["S", "M", "L"];

    [Fact]
    public void CreateProductOption_WithFullValidData_ShouldSucceed()
    {
        var productOption = ProductOption.Create(ValidTitle, ValidValues);

        Assert.Equal(ValidTitle, productOption.Title);
        Assert.Equal(ValidValues, productOption.Values.Select(v => v.Value));
    }

    [Fact]
    public void CreateProductOption_WithUntrimmedTitle_ShouldTrim()
    {
        var untrimmedTitle = "    Size    ";
        var productOption = ProductOption.Create(untrimmedTitle, ValidValues);

        Assert.Equal(ValidTitle, productOption.Title);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateProductOption_WithEmptyOrWhiteSpaceTitle_ShouldThrow(string invalidTitle)
    {
        var ex = AssertCreateThrows(inputTitle: invalidTitle);
        Assert.Equal($"String cannot be null or white space. (Parameter 'title')", ex.Message);
    }

    [Fact]
    public void CreateProductOption_WithEmptyValuesCollection_ShouldThrow()
    {
        var emptyValuesCollection = new Collection<string>() { };
        var ex = AssertCreateThrows(inputValues: emptyValuesCollection);
        Assert.Equal("Collection must contain at least 1 item(s). (Parameter 'values')", ex.Message);
    }

    [Fact]
    public void CreateProductOption_WithDuplicateValues_ShouldThrow()
    {
        var duplicateValues = new Collection<string> { "S", "S", "M", "L" };

        var ex = AssertCreateThrows(inputValues: duplicateValues);
        Assert.Equal($"Collection cannot contain duplicate Strings. (Value: {duplicateValues[0]}) (Parameter 'values')", ex.Message);
    }

    //Child

    [Theory]
    [InlineData("", "S", "L")]
    [InlineData(" ", "M", "XL")]
    public void CreateProductOption_WithEmptyOrWhiteSpaceValues_ShouldThrow(string invalidValue, string valid1, string valid2)
    {
        var invalidValues = new List<string> { invalidValue, valid1, valid2 };

        var ex = AssertCreateThrows(inputValues: invalidValues);
        Assert.Equal("String cannot be null or white space. (Parameter 'value')", ex.Message);
    }

    private static ArgumentException AssertCreateThrows(string? inputTitle = null, ICollection<string>? inputValues = null)
    {
        var title = inputTitle ?? ValidTitle;
        var values = inputValues ?? ValidValues;

        return Assert.Throws<ArgumentException>(() => ProductOption.Create(title, values));
    }
}