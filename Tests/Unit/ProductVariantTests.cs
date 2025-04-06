using System.Collections.ObjectModel;
using Core.Entities.ProductEntities;
using Core.Interfaces.Contracts;

namespace Tests.Unit;

public class ProductVariantTests
{
    private static readonly decimal ValidPrice = 199;
    private static readonly decimal NegativePrice = -99;
    private static readonly decimal ComparePrice = 349;

    private static readonly ICollection<TestAttributeInput> ValidAttributeSource =
    [
        new() { Title = "Size", Value = "Medium" },
        new() { Title = "Color", Value = "Black"}
    ];

    [Fact]
    public void CreateWithoutAttributes_WithValidData_ShouldSucceed()
    {
        var variant = ProductVariant.CreateWithoutAttributes(ValidPrice, ComparePrice);

        Assert.Equal(ValidPrice, variant.Price.Amount);
        Assert.Equal(ComparePrice, variant.ComparePrice?.Amount);
    }

    [Fact]
    public void CreateWithoutAttributes_WithNegativePrice_ShouldThrow()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
        {
            ProductVariant.CreateWithoutAttributes(NegativePrice);
        });

        Assert.Equal($"Decimal cannot be negative. (Was {NegativePrice}) (Parameter 'amount')", ex.Message);
    }

    [Fact]
    public void CreateWithoutAttributes_WithZeroComparePrice_ShouldSetComparePriceToNull()
    {
        var variant = ProductVariant.CreateWithoutAttributes(ValidPrice, inputComparePrice: 0);

        Assert.NotNull(variant.Price);
        Assert.Equal(ValidPrice, variant.Price.Amount);
        Assert.Null(variant.ComparePrice);
    }

    [Fact]
    public void CreateWithoutAttributes_ShouldInitializeEmptyAttributesList()
    {
        var variant = ProductVariant.CreateWithoutAttributes(ValidPrice);

        Assert.NotNull(variant.Attributes);
        Assert.Empty(variant.Attributes);
    }

    [Fact]
    public void CreateWithAttributes_WithValidData_ShouldSucceed()
    {
        var productImage = new ProductImage()
        {
            Url = "Some image url.",
            AltText = "Some image alt text."
        };

        var variant = ProductVariant.CreateWithAttributes(ValidAttributeSource, ValidPrice, ComparePrice, productImage);

        Assert.Equal(ValidPrice, variant.Price.Amount);
        Assert.Equal(ComparePrice, variant.ComparePrice?.Amount);
        Assert.Equal(ValidAttributeSource.Count, variant.Attributes.Count);
        Assert.Equal(productImage.Url, variant.Image?.Url);

        foreach (var attribute in ValidAttributeSource)
        {
            Assert.Contains(variant.Attributes, a => a.Title == attribute.Title && a.Value == attribute.Value);
        }
    }

    [Fact]
    public void CreateWithAttributes_WithNegativePrice_ShouldThrow()
    {
        var ex = AssertCreateWithAttributesThrows(inputPrice: NegativePrice);
        Assert.Equal($"Decimal cannot be negative. (Was {NegativePrice}) (Parameter 'amount')", ex.Message);
    }

    [Fact]
    public void CreateWithAttributes_WithZeroComparePrice_ShouldSetComparePriceToNull()
    {
        var variant = ProductVariant.CreateWithAttributes(ValidAttributeSource, ValidPrice, inputComparePrice: 0);

        Assert.NotNull(variant.Price);
        Assert.Equal(ValidPrice, variant.Price.Amount);
        Assert.Null(variant.ComparePrice);
    }

    [Fact]
    public void CreateWithAttributes_WithEmptyAttributeSource_ShouldThrow()
    {
        var ex = AssertCreateWithAttributesThrows(inputAttributeSource: []);
        Assert.Equal("Collection must contain at least 1 item(s). (Parameter 'attributeSource')", ex.Message);
    }

    [Fact]
    public void CreateWithAttributes_WithDuplicateAttributeSourceTitles_ShouldThrow()
    {
        var attributeSource = new Collection<TestAttributeInput>
        {
            new() { Title = "Size", Value = "Medium" },
            new() { Title = "Size", Value = "Medium" }
        };

        var ex = AssertCreateWithAttributesThrows(inputAttributeSource: attributeSource);
        Assert.Equal($"Collection cannot contain duplicate values. (Value: {attributeSource[0].Title}) (Parameter 'attributeSource')", ex.Message);
    }

    [Theory]
    [InlineData(" ", "", "Title")]
    [InlineData(" ", "Valid", "Title")]
    [InlineData("", "Valid", "Title")]
    [InlineData("Valid", " ", "Value")]
    [InlineData("Valid", "", "Value")]
    public void CreateWithAttributes_WithEmptyOrWhitespaceTitleOrValue_ShouldThrow(string title, string value, string expectedParamName)
    {
        var attributeSource = new List<TestAttributeInput>
    {
        new() { Title = "Size", Value = "Medium" },
        new() { Title = title, Value = value }
    };

        var ex = AssertCreateWithAttributesThrows(attributeSource);

        Assert.Equal($"String cannot be null or white space. (Parameter '{expectedParamName}')", ex.Message);
    }

    private static ArgumentException AssertCreateWithAttributesThrows(ICollection<TestAttributeInput>? inputAttributeSource = null, decimal? inputPrice = null)
    {
        var attributeSource = inputAttributeSource ?? ValidAttributeSource;
        var price = inputPrice ?? ValidPrice;

        return Assert.Throws<ArgumentException>(() =>
        {
            ProductVariant.CreateWithAttributes(attributeSource, price);
        });
    }
}

public class TestAttributeInput : IVariantAttributeCreate
{
    public string Title { get; set; } = "";
    public string Value { get; set; } = "";
}