using Pricord.Domain.Authentication.ValueObjects;
using Pricord.Domain.Common.Models;

namespace Pricord.Domain.Authentication;

public sealed class Token : Entity<TokenId, Guid>
{
    public string Value { get; private set; }
    public DateTime Expiry { get; private set; }

    public UserId UserId { get; private set; } = null!;
    public User User { get; private set; } = null!;

    private Token(string value, DateTime expiry) : base(TokenId.Create())
    {
        Value = value;
        Expiry = expiry;
    }

    public static Token Create(string value, DateTime expiry)
    {
        return new Token(value, expiry);
    }

    public void UpdateExpiration(DateTime dateTime)
    {
        Expiry = dateTime;
    }
}