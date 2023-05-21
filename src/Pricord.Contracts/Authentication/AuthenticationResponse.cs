using Pricord.Contracts.Common.Abstractions;

namespace Pricord.Contracts.Authentication;

public sealed record AuthenticationResponse(
    string AccessToken,
    string RefreshToken,
    UserDto User) : IResponse;