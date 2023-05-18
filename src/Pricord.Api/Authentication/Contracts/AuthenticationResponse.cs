using Pricord.Application.Api.Contracts.Dtos;

namespace Pricord.Api.Authentication.Contracts;

public sealed record AuthenticationResponse(
    string AccessToken,
    string RefreshToken,
    UserDto User);