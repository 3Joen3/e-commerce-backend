
using Core.Utils;

namespace Core.ValueObjects;

public class Price : ValueObject
{
    public Money Money { get; }

    public decimal Amount => Money.Amount;
    public string Currency => Money.Currency;

    public Price(decimal amount)
    {
        Guard.AgainstNegative(amount, nameof(amount));
        Money = new Money(amount);
    }

    private Price() { Money = null!; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Money;
    }

    public override string ToString() => Money.ToString();
}