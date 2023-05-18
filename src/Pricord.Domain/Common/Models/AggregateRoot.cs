using Pricord.Domain.Common.Interfaces;

namespace Pricord.Domain.Common.Models;

public abstract class AggregateRoot<TId, TIdType> : Entity<TId, TIdType>, IAuditable
    where TId : EntityId<TIdType>
{
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModified { get; set; }

    protected AggregateRoot(TId id) : base(id)
    {
    }
}