namespace Pricord.Domain.Units.ValueObjects;

public sealed record Rank
{
    public int Value { get; }

    private Rank(int value)
    {
        Value = value;
    }

    public static Rank Create(int value)
    {
        return new Rank(value);
    }
}