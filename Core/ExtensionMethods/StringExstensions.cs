using System.Text.RegularExpressions;

namespace Core.ExtensionMethods;

public static partial class StringExstensions
{
    public static string Sluggify(this string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;

        input = input.ToLowerInvariant()
        .Replace("å", "a")
        .Replace("ä", "a")
        .Replace("ö", "o")
        .Replace("/", "-");

        input = MatchInvalidSlugCharacters().Replace(input, "");

        var slug = MatchNonAlphanumerics().Replace(input, "-");
        slug = MatchConsecutiveDashes().Replace(slug, "-");
        return slug.Trim('-');
    }

    [GeneratedRegex(@"[^a-z0-9\s-]")]
    private static partial Regex MatchInvalidSlugCharacters();

    [GeneratedRegex(@"[^a-z0-9]")]
    private static partial Regex MatchNonAlphanumerics();

    [GeneratedRegex("-{2,}")]
    private static partial Regex MatchConsecutiveDashes();
}