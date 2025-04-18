using Core.Utils;
using Core.ValueObjects;

namespace Core.Entities.AbstractEntities;

public abstract class PriceEntity : BaseEntity
{
    public decimal Amount { get; private set; }
    public Currency Currency { get; private set; }

    protected PriceEntity(decimal amount, Currency currency)
    {
        Guard.AgainstNegative(amount, nameof(amount));

        Amount = amount;
        Currency = currency;
    }

    protected PriceEntity() { Currency = null!; }
}