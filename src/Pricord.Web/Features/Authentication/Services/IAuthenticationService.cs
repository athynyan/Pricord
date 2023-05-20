using Pricord.Application.Authentication.Contracts;

namespace Pricord.Web.Features.Authentication.Services;

public interface IAuthenticationService
{
    Task<UserDto?> AuthenticateAsync(string username, string password);
    Task<bool> RegisterAsync(string username, string password, string ConfirmPassword, string? email);
}

