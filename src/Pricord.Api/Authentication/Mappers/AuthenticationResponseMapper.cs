using Pricord.Api.Authentication.Contracts;
using Pricord.Application.Authentication.Contracts;

namespace Pricord.Api.Authentication.Mappers;

internal static class AuthenticationResponseMapper
{
    internal static AuthenticationResponse ToResponse(this AuthenticationResult result) 
        => new AuthenticationResponse(result.AccessToken, result.RefreshToken, result.User.ToDto());
}