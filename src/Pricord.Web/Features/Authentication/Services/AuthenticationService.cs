using System.Net;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Pricord.Contracts.Authentication;
using Pricord.Contracts.Common.Constants;

namespace Pricord.Web.Features.Authentication.Services;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly ProtectedLocalStorage _localStorage;
    private readonly ProtectedSessionStorage _sessionStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationService(
        IHttpClientFactory httpClientFactory,
        ProtectedLocalStorage localStorageService,
        ProtectedSessionStorage sessionStorageService,
        AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClientFactory.CreateClient("Pricord.Api");
        _localStorage = localStorageService;
        _sessionStorage = sessionStorageService;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<AuthenticationResponse?> AuthenticateAsync(string username, string password)
    {
        var request = new LoginRequest(username, password);
        var result = await _httpClient.PostAsJsonAsync(AuthenticationEndpoints.LoginEndpoint, request);

        if (result.StatusCode == HttpStatusCode.Unauthorized)
            return null;
        
        var authenticationResponse =  await result.Content.ReadFromJsonAsync<AuthenticationResponse>();
        await SaveAuthenticationResult(authenticationResponse);

        ((JwtAuthenticationStateProvider)_authenticationStateProvider)
            .NotifyUserAuthentication(authenticationResponse!.AccessToken);

        return authenticationResponse;
    }

    public async Task<AuthenticationResponse?> AuthenticateWithRefreshToken(string refreshToken)
    {
        var request = new RefreshRequest(refreshToken);
        var result = _httpClient.PostAsJsonAsync(AuthenticationEndpoints.RefreshEndpoint, request);

        if (result.Result.StatusCode == HttpStatusCode.Unauthorized)
            return null;

        var authenticationResponse = await result.Result.Content.ReadFromJsonAsync<AuthenticationResponse>();
        await SaveAuthenticationResult(authenticationResponse);

        ((JwtAuthenticationStateProvider)_authenticationStateProvider)
            .NotifyUserAuthentication(authenticationResponse!.AccessToken);

        return authenticationResponse;
    }

    public async Task LogoutAsync()
    {
        await Task.WhenAll(
            _localStorage.DeleteAsync("refresh_token").AsTask(),
            _sessionStorage.DeleteAsync("access_token").AsTask(),
            _sessionStorage.DeleteAsync("user").AsTask());
    }

    public async Task<bool> RegisterAsync(string username, string password, string ConfirmPassword, string? email)
    {
        if (password != ConfirmPassword)
            return false;
        
        var request = new RegisterRequest(username, password, email);

        var response = await _httpClient.PostAsJsonAsync(AuthenticationEndpoints.RegisterEndpoint, request);
        
        var authenticationResult = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();
        await SaveAuthenticationResult(authenticationResult);

        ((JwtAuthenticationStateProvider)_authenticationStateProvider)
            .NotifyUserAuthentication(authenticationResult!.AccessToken);

        return response.IsSuccessStatusCode;
    }

    public async Task TryAuthenticateWithRefreshTokenAsync()
    {
        Console.WriteLine("Trying to authenticate with refresh token");

        var refreshToken = await _localStorage.GetAsync<string>("refresh_token");
        if (refreshToken.Value is null)
            return;

        await AuthenticateWithRefreshToken(refreshToken.Value);
    }

    private async Task SaveAuthenticationResult(AuthenticationResponse? authenticationResult)
    {
        await Task.WhenAll(
            _localStorage.SetAsync("refresh_token", authenticationResult!.RefreshToken).AsTask(),
            _sessionStorage.SetAsync("access_token", authenticationResult!.AccessToken).AsTask(),
            _sessionStorage.SetAsync("user", authenticationResult!.User).AsTask());
    }
}
