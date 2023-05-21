using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Pricord.Web.Features.Authentication.Services;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private readonly ClaimsPrincipal _anonymous;

    public JwtAuthenticationStateProvider(
        ProtectedSessionStorage sessionStorage,
        ProtectedLocalStorage localStorage)
    {
        _sessionStorage = sessionStorage;
        _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
    }

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var accessToken = await _sessionStorage.GetAsync<string>("access_token");

        if (string.IsNullOrWhiteSpace(accessToken.Value))
        {
            return new AuthenticationState(_anonymous);
        }

        var user = new ClaimsPrincipal(new ClaimsIdentity(
            ParseClaimsFromJwt(accessToken.Value),
            "jwt",
            ClaimTypes.Name,
            ClaimTypes.Role));

        return new AuthenticationState(user);
    }

    public void NotifyUserAuthentication(string token)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(
            ParseClaimsFromJwt(token),
            "jwt",
            ClaimTypes.Name,
            ClaimTypes.Role));

        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(new AuthenticationState(_anonymous));
        NotifyAuthenticationStateChanged(authState);
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string value)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(value);
            
            return token.Claims;
        }
        catch (Exception)
        {
            return Enumerable.Empty<Claim>();
        }
    }
}
