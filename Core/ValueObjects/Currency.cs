using Core.Utils;

namespace Core.ValueObjects;

public class Currency : ValueObject
{
    public string Code { get; }

    public Currency(string code)
    {
        Guard.AgainstNullOrWhiteSpace(code, nameof(code));

        if (code.Length != 3) throw new ArgumentException("Invalid currency code.", nameof(code));

        Code = code.ToUpperInvariant();
    }
    
    private Currency() { Code = null!; }
    
    public static readonly Currency SEK = new("SEK");

    public override string ToString() => Code;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Code;
    }
}