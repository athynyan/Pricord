using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Pricord.Web.Features.Authentication.Services;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ISessionStorageService _sessionStorage;
    private readonly ClaimsPrincipal _anonymous;

    public JwtAuthenticationStateProvider(ISessionStorageService sessionStorage)
    {
        _sessionStorage = sessionStorage;
        _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
    }

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var accessToken = await _sessionStorage.GetItemAsync<string>("access_token");

        if (string.IsNullOrWhiteSpace(accessToken))
        {
            return new AuthenticationState(_anonymous);
        }

        var user = new ClaimsPrincipal(new ClaimsIdentity(
            ParseClaimsFromJwt(accessToken),
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
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadJwtToken(value);
        
        return token.Claims;
    }
}
