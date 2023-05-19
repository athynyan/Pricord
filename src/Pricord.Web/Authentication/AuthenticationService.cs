using System.Net;
using Pricord.Web.Authentication.Contracts;

namespace Pricord.Web.Authentication;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDto?> AuthenticateAsync(string username, string password)
    {
        var request = new LoginRequest(username, password);
        var result = await _httpClient.PostAsJsonAsync("login", request);

        if (result.StatusCode == HttpStatusCode.Unauthorized)
            return null;
        
        var authenticationResponse =  await result.Content.ReadFromJsonAsync<AuthenticationResponse>();

        return authenticationResponse?.User;
    }    
}
