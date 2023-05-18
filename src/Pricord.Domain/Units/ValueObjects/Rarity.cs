namespace Pricord.Domain.Units.ValueObjects;

public sealed record Rarity
{
    public int Value { get; }

    private Rarity(int value)
    {
        Value = value;
    }

    public static Rarity Create(int value)
    {
        return new(value);
    }
}