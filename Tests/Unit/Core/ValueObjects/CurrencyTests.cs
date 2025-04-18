using Core.ValueObjects;

namespace Tests.Unit.Core.ValueObjects;

public class CurrencyTests
{
    [Theory]
    [InlineData("SEK")]
    [InlineData("USD")]
    [InlineData("EUR")]
    public void CreateCurrency_WithValidCode_ShouldSucceed(string validCode)
    {
        var currency = new Currency(validCode);
        Assert.Equal(validCode, currency.Code);
    }

    [Theory]
    [InlineData("sek", "SEK")]
    [InlineData("usD", "USD")]
    [InlineData("EUr", "EUR")]
    public void CreateCurrency_WithValidLowercaseCode_ShouldNormalizeToUppercase(string validLowercaseCode, string expected)
    {
        var currency = new Currency(validLowercaseCode);
        Assert.Equal(expected, currency.Code);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void CreateCurrency_WithEmptyOrWhiteSpaceCode_ShouldThrow(string invalidCode)
    {
        var ex = Assert.Throws<ArgumentException>(() => new Currency(invalidCode));
        Assert.Equal("String cannot be null or white space. (Parameter 'code')", ex.Message);
    }

    [Theory]
    [InlineData("A")]
    [InlineData("AB")]
    [InlineData("ABCD")]
    [InlineData("ABCDE")]
    public void CreateCurrency_WithInvalidLengthCode_ShouldThrow(string invalidCode)
    {
        var ex = Assert.Throws<ArgumentException>(() => new Currency(invalidCode));
        Assert.Equal("Invalid currency code. (Parameter 'code')", ex.Message);
    }
}