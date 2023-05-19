namespace Pricord.Web.Authentication.Contracts;

public sealed record AuthenticationResponse(string AccessToken, string RefreshToken, UserDto User);