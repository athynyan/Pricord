using Pricord.Domain.Authentication.Enums;
using Pricord.Domain.Authentication.ValueObjects;
using Pricord.Domain.Common.Models;
using Pricord.Domain.Common.ValueObjects;

namespace Pricord.Domain.Authentication;

public sealed class User : AggregateRoot<UserId, Guid>
{   
    private HashSet<Token> _tokens = new HashSet<Token>();

    public Name Name { get; set; } = default!;
    public Password Password { get; set; } = default!;
    public Role Role { get; set; } = default!;
    public Email? Email { get; set; } = default!;

    public IEnumerable<Token> Tokens => _tokens;

    private User() : base(UserId.Create())
    {
    }

    public static User Create(
        Name name, 
        Password password, 
        Role role, 
        Email? email)
    {
        return new User
        {
            Name = name,
            Password = password,
            Role = role,
            Email = email
        };
    }

    public void AddToken(Token token)
    {
        _tokens.Add(token);
    }

    public void RemoveToken(string token)
    {
        _tokens.RemoveWhere(t => t.Value == token);
    }

    public void ClearTokens()
    {
        _tokens.Clear();
    }
}