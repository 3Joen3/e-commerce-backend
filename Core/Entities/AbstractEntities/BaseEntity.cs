namespace Core.Entities.AbstractEntities;

public abstract class BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
}
