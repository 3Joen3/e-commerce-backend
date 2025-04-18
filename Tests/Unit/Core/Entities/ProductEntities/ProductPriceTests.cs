using Core.Entities.ProductEntities;
using Core.ValueObjects;

namespace Tests.Unit.Core.Entities.ProductEntities;

public class ProductPriceTests
{
    [Fact]
    public void CreateProduct_WithFullValidData_ShouldSucceed()
    {
        var amount = 100;
        var currency = Currency.SEK;

        var price = new ProductPrice(amount, currency);

        Assert.Equal(amount, price.Amount);
        Assert.Equal(currency, price.Currency);
    }

    [Theory]
    [InlineData(-99)]
    [InlineData(-0.99)]
    public void CreateProductPrice_WithNegativeAmount_ShouldThrow(decimal invalidAmount)
    {
        var currency = Currency.SEK;

        var ex = Assert.Throws<ArgumentException>(() => new ProductPrice(invalidAmount, currency));
        Assert.Equal($"Decimal cannot be negative. (Was {invalidAmount}) (Parameter 'amount')", ex.Message);
    }
}