using System.Net;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Pricord.Contracts.Authentication;
using Pricord.Contracts.Common.Constants;

namespace Pricord.Web.Features.Authentication.Services;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly ISessionStorageService _sessionStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationService(
        HttpClient httpClient,
        ILocalStorageService localStorageService,
        ISessionStorageService sessionStorageService,
        AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClient;
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
        await _localStorage.ClearAsync();

        ((JwtAuthenticationStateProvider)_authenticationStateProvider)
            .NotifyUserLogout();
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

        var refreshToken = await _localStorage.GetItemAsync<string>("refresh_token");
        if (refreshToken is null)
            return;

        await AuthenticateWithRefreshToken(refreshToken);
    }

    private async Task SaveAuthenticationResult(AuthenticationResponse? authenticationResult)
    {
        await Task.WhenAll(
            _localStorage.SetItemAsync("refresh_token", authenticationResult!.RefreshToken).AsTask(),
            _sessionStorage.SetItemAsync("access_token", authenticationResult!.AccessToken).AsTask(),
            _sessionStorage.SetItemAsync("user", authenticationResult!.User).AsTask());
    }
}
