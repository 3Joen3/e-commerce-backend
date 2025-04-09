using System.Collections.ObjectModel;
using Core.Entities.ProductEntities;
using Core.Interfaces.Contracts;

namespace Tests.Unit.Core.Entities.ProductEntities;

public class ProductVariantTests
{
    private static readonly decimal ValidPrice = 199;
    private static readonly decimal ValidComparePrice = 349;

    private static readonly ICollection<TestAttributeInput> ValidAttributeSource =
    [
        new() { Title = "Size", Value = "Medium" },
        new() { Title = "Color", Value = "Black"}
    ];

    [Fact]
    public void CreateWithoutAttributes_WithValidData_ShouldSucceed()
    {
        var variant = ProductVariant.CreateWithoutAttributes(ValidPrice, ValidComparePrice);

        Assert.Equal(ValidPrice, variant.Price.Amount);
        Assert.Equal(ValidComparePrice, variant.ComparePrice?.Amount);
    }

    [Fact]
    public void CreateWithoutAttributes_WithNegativePrice_ShouldThrow()
    {
        var negativePrice = -99;

        var ex = Assert.Throws<ArgumentException>(() =>
        {
            ProductVariant.CreateWithoutAttributes(negativePrice);
        });

        Assert.Equal($"Decimal cannot be negative. (Was {negativePrice}) (Parameter 'amount')", ex.Message);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(0)]
    public void CreateWithoutAttributes_WithLowerOrEqualComparePrice_ShouldThrow(decimal subtract)
    {
        var ex = Assert.Throws<ArgumentException>(() =>
        {
            ProductVariant.CreateWithoutAttributes(ValidPrice, ValidPrice - subtract);
        });

        Assert.Equal($"Decimal must be greater than {ValidPrice}. (Parameter 'inputComparePrice')", ex.Message);
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
        var productImage = new ProductImage
        (
            url: "https://www.example.com/image",
            altText: "Some image alt text."
        );

        var variant = ProductVariant.CreateWithAttributes(ValidAttributeSource, ValidPrice, ValidComparePrice, productImage);

        Assert.Equal(ValidPrice, variant.Price.Amount);
        Assert.Equal(ValidComparePrice, variant.ComparePrice?.Amount);
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
        var negativePrice = -99;

        var ex = AssertCreateWithAttributesThrows(inputPrice: negativePrice);
        Assert.Equal($"Decimal cannot be negative. (Was {negativePrice}) (Parameter 'amount')", ex.Message);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(0)]
    public void CreateWithAttributes_WithLowerOrEqualComparePrice_ShouldThrow(decimal subtract)
    {
        var ex = AssertCreateWithAttributesThrows(inputComparePrice: ValidPrice - subtract);
        Assert.Equal($"Decimal must be greater than {ValidPrice}. (Parameter 'inputComparePrice')", ex.Message);
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
        Assert.Equal($"Collection cannot contain duplicate Strings. (Value: {attributeSource[0].Title}) (Parameter 'attributeSource')", ex.Message);
    }

    [Theory]
    [InlineData(" ", "", "title")]
    [InlineData(" ", "Valid", "title")]
    [InlineData("", "Valid", "title")]
    [InlineData("Valid", " ", "value")]
    [InlineData("Valid", "", "value")]
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

    private static ArgumentException AssertCreateWithAttributesThrows(ICollection<TestAttributeInput>? inputAttributeSource = null, decimal? inputPrice = null, decimal? inputComparePrice = null)
    {
        var attributeSource = inputAttributeSource ?? ValidAttributeSource;
        var price = inputPrice ?? ValidPrice;
        var comparePrice = inputComparePrice ?? ValidComparePrice;

        return Assert.Throws<ArgumentException>(() =>
        {
            ProductVariant.CreateWithAttributes(attributeSource, price, comparePrice);
        });
    }
}

public class TestAttributeInput : IVariantAttributeCreate
{
    public string Title { get; set; } = "";
    public string Value { get; set; } = "";
}