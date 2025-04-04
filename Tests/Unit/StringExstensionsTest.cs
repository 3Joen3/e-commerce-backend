using Core.ExtensionMethods;

namespace Tests.Unit;

public class StringExstensionsTests
{
    [Theory]
    [InlineData("    Sentence with    several spaces    ", "sentence-with-several-spaces")]
    [InlineData("123 Sentence with numbers 456", "123-sentence-with-numbers-456")]
    [InlineData("Sentence WiTh rAndom CAPS", "sentence-with-random-caps")]
    [InlineData("Sente.nce. with || å ä ö special charac🌍te>rs ! |", "sentence-with-a-a-o-special-characters")]
    [InlineData("Sentence with blue/red in it", "sentence-with-blue-red-in-it")]
    [InlineData("Sentence--with--multiple---dashes", "sentence-with-multiple-dashes")]
    [InlineData("already-slugged-sentence", "already-slugged-sentence")]
    [InlineData("", "")]
    public void Sluggify_ShouldReturnExpectedSlug(string input, string expected)
    {
        var result = input.Sluggify();
        Assert.Equal(result, expected);
    }
}