namespace Core.Utils;

public static class Guard
{
    public static void AgainstDuplicateString(IEnumerable<string> values, string parameterName)
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var value in values)
        {
            if (!seen.Add(value))
                throw new ArgumentException($"Collection cannot contain duplicate Strings. (Value: {value})", parameterName);
        }
    }

    public static void AgainstLowerValue(decimal value, decimal comparedTo, string parameterName)
    {
        if (value <= comparedTo) throw new ArgumentException($"Decimal must be greater than {comparedTo}.", parameterName);
    }

    public static void AgainstNegative(decimal value, string parameterName)
    {
        if (value < 0) throw new ArgumentException($"Decimal cannot be negative. (Was {value})", parameterName);
    }

    public static void AgainstNullOrWhiteSpace(string input, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException($"String cannot be null or white space.", parameterName);
    }

    public static void AgainstEmptyCollection<T>(this ICollection<T>? collection, string parameterName)
    {
        AgainstTooFewItems(collection, 1, parameterName);
    }

    public static void AgainstTooFewItems<T>(ICollection<T>? collection, int minCount, string parameterName)
    {
        if (collection == null || collection.Count < minCount)
            throw new ArgumentException($"Collection must contain at least {minCount} item(s).", parameterName);
    }
}