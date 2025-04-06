namespace Core.Utils;

public static class Guard
{
    public static void AgainstNegative(decimal value, string parameterName)
    {
        if (value < 0) throw new ArgumentException($"Value cannot be negative. (Was {value})", parameterName);
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