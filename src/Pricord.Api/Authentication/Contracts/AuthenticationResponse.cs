using Pricord.Application.Authentication.Contracts;

namespace Pricord.Api.Authentication.Contracts;

public sealed record AuthenticationResponse(
    string AccessToken,
    string RefreshToken,
    UserDto User);