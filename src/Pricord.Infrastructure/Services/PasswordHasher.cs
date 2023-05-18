using System.Text;
using Pricord.Application.Common.Services;
using Pricord.Domain.Authentication.ValueObjects;

namespace Pricord.Infrastructure.Services;

internal sealed class PasswordHasher : IPasswordHasher
{
    public Password HashPassword(string password)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

        var bytes = Encoding.UTF8.GetBytes(hash);
        var base64 = Convert.ToBase64String(bytes);

        return Password.Create(base64);
    }

    public bool VerifyPassword(string password, Password existingPassword)
    {
        var bytes = Convert.FromBase64String(existingPassword.Hash);
        var hash = Encoding.UTF8.GetString(bytes);

        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}