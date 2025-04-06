using System.Globalization;

namespace Core.ValueObjects;

public class Money(decimal amount) : ValueObject
{
    public decimal Amount { get; } = amount;
    public string Currency { get; } = "SEK";

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString()
    {
        var culture = CultureInfo.GetCultureInfo("sv-SE");
        return $"{Amount.ToString("N2", culture)} {Currency}";
    }
}