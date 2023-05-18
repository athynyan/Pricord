using Pricord.Domain.Authentication.ValueObjects;
using Pricord.Domain.Common.Models;

namespace Pricord.Domain.Authentication;

public sealed class Token : Entity<TokenId, Guid>
{
    public string Value { get; init; }
    public DateTime Expiry { get; init; }

    public UserId UserId { get; init; } = null!;
    public User User { get; init; } = null!;

    private Token(string value, DateTime expiry) : base(TokenId.Create())
    {
        Value = value;
        Expiry = expiry;
    }

    public static Token Create(string value, DateTime expiry)
    {
        return new Token(value, expiry);
    }
}