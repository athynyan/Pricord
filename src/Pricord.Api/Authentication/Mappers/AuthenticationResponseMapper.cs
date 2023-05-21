using Pricord.Application.Authentication.Models;
using Pricord.Application.Authentication.Mappers;
using Pricord.Contracts.Authentication;

namespace Pricord.Api.Authentication.Mappers;

internal static class AuthenticationResponseMapper
{
    internal static AuthenticationResponse ToResponse(this AuthenticationResult result) 
        => new AuthenticationResponse(result.AccessToken, result.RefreshToken, result.User.ToDto());
}