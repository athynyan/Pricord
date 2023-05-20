using System.Net;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Pricord.Application.Authentication.Contracts;
using Pricord.Application.Common.Constants;
using Pricord.Web.Features.Authentication.Contracts;

namespace Pricord.Web.Features.Authentication.Services;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly ProtectedLocalStorage _localStorageService;
    private readonly ProtectedSessionStorage _sessionStorageService;

    public AuthenticationService(IHttpClientFactory httpClientFactory, ProtectedLocalStorage localStorageService, ProtectedSessionStorage sessionStorageService)
    {
        _httpClient = httpClientFactory.CreateClient("Pricord.Api");
        _localStorageService = localStorageService;
        _sessionStorageService = sessionStorageService;
    }

    public async Task<AuthenticationResult?> AuthenticateAsync(string username, string password)
    {
        var request = new LoginRequest(username, password);
        var result = await _httpClient.PostAsJsonAsync(AuthenticationEndpoints.LoginEndpoint, request);

        if (result.StatusCode == HttpStatusCode.Unauthorized)
            return null;
        
        var authenticationResult =  await result.Content.ReadFromJsonAsync<AuthenticationResult>();
        await SaveAuthenticationResult(authenticationResult);

        Console.WriteLine((await _localStorageService.GetAsync<string>("access_token")).Value);

        return authenticationResult;
    }

    public async Task<AuthenticationResult?> AuthenticateWithRefreshToken(string refreshToken)
    {
        var request = new RefreshTokenRequest(refreshToken);

        var response = _httpClient.PostAsJsonAsync(AuthenticationEndpoints.RefreshEndpoint, request);

        if (response.Result.StatusCode == HttpStatusCode.Unauthorized)
            return null;

        var authenticationResult = await response.Result.Content.ReadFromJsonAsync<AuthenticationResult>();
        await SaveAuthenticationResult(authenticationResult);

        return authenticationResult;
    }

    public async Task<bool> RegisterAsync(string username, string password, string ConfirmPassword, string? email)
    {
        if (password != ConfirmPassword)
            return false;
        
        var request = new RegisterRequest(username, password, email);

        var result = await _httpClient.PostAsJsonAsync(AuthenticationEndpoints.RegisterEndpoint, request);
        
        return result.IsSuccessStatusCode;
    }

    private async Task SaveAuthenticationResult(AuthenticationResult? authenticationResult)
    {
        await Task.WhenAll(
            _localStorageService.SetAsync("refresh_token", authenticationResult!.RefreshToken).AsTask(),
            _sessionStorageService.SetAsync("access_token", authenticationResult!.AccessToken).AsTask(),
            _sessionStorageService.SetAsync("user", authenticationResult!.User).AsTask());
    }
}
