namespace Pricord.Domain.Authentication.ValueObjects;

public sealed record Password
{
    public string Hash { get; } = default!;

    private Password(string hash)
    {
        Hash = hash;
    }

    private Password() {}

    public static Password Create(string value)
    {
        return new Password(value);
    }
}