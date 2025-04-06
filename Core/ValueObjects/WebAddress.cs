
namespace Core.ValueObjects;

public class WebAddress : ValueObject
{
    public string Url { get; }

    public WebAddress(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) throw new ArgumentException("URL cannot be empty.", nameof(url));

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
        {
            throw new ArgumentException($"Invalid URL: {url}.", nameof(url));
        }

        Url = url;
    }

    public override string ToString() => Url;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Url.ToUpperInvariant();
    }
}