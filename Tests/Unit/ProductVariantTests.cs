using Core.Entities.ProductEntities;

namespace Tests.Unit;

public class ProductVariantTests
{
    [Fact]
    public void CreateWithoutAttributes_WithValidData_ShouldSucceed()
    {
        decimal price = 199;
        decimal comparePrice = 349;

        var variant = ProductVariant.CreateWithoutAttributes(price, comparePrice);

        Assert.Equal(price, variant.Price);
        Assert.Equal(comparePrice, variant.ComparePrice);
    }

    [Fact]
    public void CreateWithoutAttributes_WithNegativePrice_ShouldThrow()
    {
        decimal price = -99;

        var ex = Assert.Throws<ArgumentException>(() =>
        {
            ProductVariant.CreateWithoutAttributes(price);
        });

        Assert.Equal("Price cannot be negative.", ex.Message);
    }

    [Fact]
    public void CreateWithoutAttributes_ShouldInitializeEmptyAttributesList()
    {
        var variant = ProductVariant.CreateWithoutAttributes(199);

        Assert.NotNull(variant.Attributes);
        Assert.Empty(variant.Attributes);
    }
}