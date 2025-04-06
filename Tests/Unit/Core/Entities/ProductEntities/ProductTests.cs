using Core.Entities.ProductEntities;

namespace Tests.Unit;

public class ProductTests
{
    [Theory]
    [InlineData("Märke Hoodie för män", "marke-hoodie-for-man")]
    [InlineData("adidas Originals | CAMPUS 00s - Låga sneakers", "adidas-originals-campus-00s-laga-sneakers")]
    [InlineData("Rogue St Max 24 - Stål/Grafit", "rogue-st-max-24-stal-grafit")]
    public void SettingTitle_ShouldSetSlugCorrectly(string title, string expectedSlug)
    {
        var product = new Product
        {
            Title = title,
            Variants = []
        };

        Assert.Equal(expectedSlug, product.Slug);
    }
}