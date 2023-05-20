namespace Pricord.Application.Authentication.Contracts;

public sealed record AuthenticationResult(
	string AccessToken,
	string RefreshToken,
	UserDto User);