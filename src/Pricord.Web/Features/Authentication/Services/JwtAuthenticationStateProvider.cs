using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Pricord.Web.Features.Authentication.Services;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private readonly ProtectedLocalStorage _localStorage;
    private readonly IAuthenticationService _authenticationService;

    private readonly ClaimsPrincipal _anonymous;

    public JwtAuthenticationStateProvider(
        ProtectedSessionStorage sessionStorage,
        ProtectedLocalStorage localStorage,
        IAuthenticationService authenticationService)
    {
        _sessionStorage = sessionStorage;
        _localStorage = localStorage;
        _authenticationService = authenticationService;
        _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
    }

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var accessToken = await _sessionStorage.GetAsync<string>("access_token");
    
        if (!string.IsNullOrWhiteSpace(accessToken.Value))
        {
            return await AuthenticateUser(accessToken.Value);
        }

        var token = await _localStorage.GetAsync<string>("refresh_token");

        if (string.IsNullOrWhiteSpace(token.Value))
        {
            return new AuthenticationState(_anonymous);
        }

        var authenticationResult = _authenticationService.AuthenticateWithRefreshToken(token.Value).Result;

        if (authenticationResult is null)
        {
            await _localStorage.DeleteAsync("refresh_token");
            return new AuthenticationState(_anonymous);
        }

        return await AuthenticateUser(authenticationResult.AccessToken);
    }

    public void NotifyUserAuthentication(string token)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(new AuthenticationState(_anonymous));
        NotifyAuthenticationStateChanged(authState);
    }

    private async Task<AuthenticationState> AuthenticateUser(string accessToken)
    {
        var claims = ParseClaimsFromJwt(accessToken);

        if (claims is null)
        {
            await _sessionStorage.DeleteAsync("access_token");
            return new AuthenticationState(_anonymous);
        }

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        return new AuthenticationState(user);
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
