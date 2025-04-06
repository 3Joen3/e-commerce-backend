using Core.Utils;
using Core.ValueObjects;

namespace Core.Entities.AbstractEntities;

public abstract class ImageEntity : BaseEntity
{
    public WebAddress Url { get; set; }
    public string AltText { get; set; } = string.Empty;

    protected ImageEntity(string url, string altText)
    {
        Guard.AgainstNullOrWhiteSpace(altText, nameof(altText));

        Url = new WebAddress(url);
        AltText = altText;
    }

    protected ImageEntity() { Url = null!; }
}