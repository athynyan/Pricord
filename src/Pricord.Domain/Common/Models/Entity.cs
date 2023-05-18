namespace Pricord.Domain.Common.Models;

public abstract class Entity<TId, TIdType> : IEquatable<Entity<TId, TIdType>>
    where TId : EntityId<TIdType>
{
    public TId Id { get; protected init; }

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId, TIdType> entity && Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity<TId, TIdType> left, Entity<TId, TIdType> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId, TIdType> left, Entity<TId, TIdType> right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public bool Equals(Entity<TId, TIdType>? other)
    {
        return Equals((object?)other);
    }
}