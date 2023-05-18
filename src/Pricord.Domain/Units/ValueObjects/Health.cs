namespace Pricord.Domain.Units.ValueObjects;

public sealed record Health
{
    public long Value { get; }

    private Health(long value)
    {
        Value = value;
    }

    public static Health Create(long value)
    {
        return new(value);
    }
}