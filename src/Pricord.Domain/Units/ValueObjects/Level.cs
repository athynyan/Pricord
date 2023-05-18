namespace Pricord.Domain.Units.ValueObjects;

public sealed record Level
{
    public int Value { get; } = default!;

    private Level(int value)
    {
        Value = value;
    }

    public static Level Create(int value)
    {
        return new(value);
    }
}