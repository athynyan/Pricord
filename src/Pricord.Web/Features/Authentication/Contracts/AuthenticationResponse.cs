using Pricord.Application.Authentication.Contracts;

namespace Pricord.Web.Features.Authentication.Contracts;

public sealed record AuthenticationResponse(string AccessToken, string RefreshToken, UserDto User);