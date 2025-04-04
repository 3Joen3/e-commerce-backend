namespace Core.Entities.AbstractEntities;

public abstract class ImageEntity : BaseEntity
{
    public required string Url { get; set; }
    public required string AltText { get; set; }
}