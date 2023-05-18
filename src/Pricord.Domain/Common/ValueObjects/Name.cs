namespace Pricord.Domain.Common.ValueObjects;

public sealed record Name
{
    public string Value { get; private init; }

    private Name(string value)
    {
        Value = value;
    }

    public static Name Create(string value)
    {
        return new Name(value);
    }
}