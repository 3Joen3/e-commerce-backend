using Core.ValueObjects;

namespace Tests.Unit.ValueObjects;

public class PriceTests
{
    [Fact]
    public void CreatePrice_WithNegativeAmount_ShouldThrow()
    {
        var amount = -99;
        var ex = Assert.Throws<ArgumentException>(() => new Price(amount));
        Assert.Equal($"Decimal cannot be negative. (Was {amount}) (Parameter 'amount')", ex.Message);
    }

    [Theory]
    [InlineData(100)]
    [InlineData(0)]
    public void CreatePrice_WithValidAmount_ShouldSucceed(decimal amount)
    {
        var price = new Price(amount);

        Assert.Equal(amount, price.Amount);
    }

    [Fact]
    public void ToString_ShouldIncludeAmountAndCurrency()
    {
        var price = new Price(199.95m);
        var result = price.ToString();

        Assert.Equal("199,95 SEK", result);
    }
}