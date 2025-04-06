using Core.ValueObjects;

namespace Tests.Unit.ValueObjects;

public class ValueObjectTests
{
    [Fact]
    public void TwoValueObjects_WithSameEqualityComponents_ShouldBeEqual()
    {
        var a = new FakeValueObject(100, "SEK");
        var b = new FakeValueObject(100, "SEK");

        Assert.Equal(a, b);
        Assert.True(a == b);
        Assert.False(a != b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    private class FakeValueObject(decimal amount, string currency) : ValueObject
    {
        public decimal Amount { get; } = amount;
        public string Currency { get; } = currency;

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}