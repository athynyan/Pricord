using Pricord.Domain.Authentication;

namespace Pricord.Application.Common.Services;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    Token GenerateRefreshToken();

    bool ValidateRefreshToken(string tokenString, Token token);
}