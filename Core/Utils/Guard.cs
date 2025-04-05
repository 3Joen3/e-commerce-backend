namespace Core.Utils;

public static class Guard
{
    public static void AgainstNegative(decimal value, string parameterName)
    {
        if (value < 0) throw new ArgumentException($"{UppercaseFirst(parameterName)} cannot be negative.");
    }

    private static string UppercaseFirst(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return "";
        return char.ToUpper(input[0]) + input[1..];
    }
}