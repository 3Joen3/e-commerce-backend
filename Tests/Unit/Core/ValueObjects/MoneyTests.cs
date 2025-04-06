using Core.ValueObjects;

namespace Tests.Unit.Core.ValueObjects;

public class MoneyTests()
{
    [Fact]
    public void ToString_ShouldIncludeAmountAndCurrency()
    {
        var money = new Money(199.95m);
        var result = money.ToString();

        Assert.Equal("199,95 SEK", result);
    }
}