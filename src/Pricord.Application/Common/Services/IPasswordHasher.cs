using Pricord.Domain.Authentication.ValueObjects;

namespace Pricord.Application.Common.Services;

public interface IPasswordHasher
{
    Password HashPassword(string password);
    bool VerifyPassword(string password, Password existingPassword);
}