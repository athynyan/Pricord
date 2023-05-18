namespace Pricord.Domain.Authentication.ValueObjects;

public sealed record Email
{
    public string Value { get; } = default!;

    private Email(string value)
    {
        Value = value;
    }

    private Email() {}

    public static Email Create(string value)
    {
        return new Email(value);
    }
}