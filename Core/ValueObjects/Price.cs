
namespace Core.ValueObjects;

public class Price : ValueObject
{
    public Money Money { get; }

    public decimal Amount => Money.Amount;
    public string Currency => Money.Currency;

    public Price(decimal amount)
    {
        if (amount < 0) throw new ArgumentException("Price cannot be negative.", nameof(amount));

        Money = new Money(amount);
    }

    private Price() { Money = null!; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Money;
    }

    public override string ToString() => Money.ToString();
}