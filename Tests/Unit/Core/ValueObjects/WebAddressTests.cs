using Core.ValueObjects;

namespace Tests.Unit.Core.ValueObjects;

public class WebAddressTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void CreatingWebAddress_WithEmptyOrWhitespace_ShouldThrow(string input)
    {
        var ex = Assert.Throws<ArgumentException>(() => new WebAddress(input));
        Assert.Equal("String cannot be null or white space. (Parameter 'url')", ex.Message);
    }

    [Theory]
    [InlineData("Just some random words.")]
    [InlineData("google.com")]
    public void CreatingWebAddress_WithInvalidUrl_ShouldThrow(string input)
    {
        var ex = Assert.Throws<ArgumentException>(() => new WebAddress(input));
        Assert.Equal($"Invalid URL: {input}. (Parameter 'url')", ex.Message);
    }

    [Theory]
    [InlineData("ftp://example.com")]
    [InlineData("mailto:test@example.com")]
    [InlineData("file:///C:/path/to/file")]
    public void CreatingWebAddress_WithInvalidSchemes_ShouldThrow(string input)
    {
        var ex = Assert.Throws<ArgumentException>(() => new WebAddress(input));
        Assert.Equal($"Invalid URL: {input}. (Parameter 'url')", ex.Message);
    }

    [Theory]
    [InlineData("https://example.com")]
    [InlineData("http://localhost:5000")]
    public void CreatingWebAddress_WithValidUrl_ShouldSucceed(string input)
    {
        var address = new WebAddress(input);
        Assert.Equal(input, address.Url);
    }

    [Fact]
    public void ToString_ShouldReturnOriginalUrl()
    {
        var url = "https://example.com/path";
        var address = new WebAddress(url);

        Assert.Equal(url, address.ToString());
    }
}