using Pricord.Web.Authentication.Contracts;

namespace Pricord.Web.Authentication;

public interface IAuthenticationService
{
    Task<UserDto?> AuthenticateAsync(string username, string password);
}

