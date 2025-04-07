using Core.Utils;
using Core.ValueObjects;

namespace Core.Entities.AbstractEntities;

public abstract class ImageEntity : BaseEntity
{
    public WebAddress Url { get; private set; }
    public string AltText { get; private set; }

    protected ImageEntity(string url, string altText)
    {
        Guard.AgainstNullOrWhiteSpace(altText, nameof(altText));

        Url = new WebAddress(url);
        AltText = altText;
    }

    protected ImageEntity() { Url = null!; AltText = null!; }
}