namespace Pricord.Domain.Common.Models;

public abstract record EntityId<T>
{
    public T Value { get; protected init; }

    protected EntityId(T value)
    {
        Value = value;
    }
}