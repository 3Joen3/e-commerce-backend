using System.Globalization;

namespace Core.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; } = "SEK";

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public Money(decimal amount) => Amount = amount;

    private Money() { }

    public override string ToString()
    {
        var culture = CultureInfo.GetCultureInfo("sv-SE");
        return $"{Amount.ToString("N2", culture)} {Currency}";
    }
}