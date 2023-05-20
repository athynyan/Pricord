using Pricord.Application.Authentication.Contracts;

namespace Pricord.Web.Features.Authentication.Services;

public interface IAuthenticationService
{
    Task<AuthenticationResult?> AuthenticateAsync(string username, string password);
    Task<AuthenticationResult?> AuthenticateWithRefreshToken(string refreshToken);
    Task<bool> RegisterAsync(string username, string password, string ConfirmPassword, string? email);
}

