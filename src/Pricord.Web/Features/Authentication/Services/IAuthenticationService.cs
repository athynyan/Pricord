using Pricord.Contracts.Authentication;

namespace Pricord.Web.Features.Authentication.Services;

public interface IAuthenticationService
{
    Task<AuthenticationResponse?> AuthenticateAsync(string username, string password);
    Task<AuthenticationResponse?> AuthenticateWithRefreshToken(string refreshToken);
    Task<bool> RegisterAsync(string username, string password, string ConfirmPassword, string? email);

    Task LogoutAsync();
}

