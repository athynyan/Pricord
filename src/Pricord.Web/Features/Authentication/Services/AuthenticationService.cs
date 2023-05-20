using System.Net;
using Pricord.Application.Authentication.Contracts;
using Pricord.Application.Common.Constants;
using Pricord.Web.Features.Authentication.Contracts;

namespace Pricord.Web.Features.Authentication.Services;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Pricord.Api");
    }

    public async Task<UserDto?> AuthenticateAsync(string username, string password)
    {
        var request = new LoginRequest(username, password);
        var result = await _httpClient.PostAsJsonAsync(AuthenticationEndpoints.LoginEndpoint, request);

        if (result.StatusCode == HttpStatusCode.Unauthorized)
            return null;
        
        var authenticationResponse =  await result.Content.ReadFromJsonAsync<AuthenticationResponse>();

        return authenticationResponse?.User;
    }

    public async Task<bool> RegisterAsync(string username, string password, string ConfirmPassword, string? email)
    {
        if (password != ConfirmPassword)
            return false;
        
        var request = new RegisterRequest(username, password, email);

        var result = await _httpClient.PostAsJsonAsync(AuthenticationEndpoints.RegisterEndpoint, request);
        
        return result.IsSuccessStatusCode;
    }
}
