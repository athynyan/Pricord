using Pricord.Domain.Authentication;

namespace Pricord.Application.Authentication.Contracts;

public sealed record AuthenticationResult(
	string AccessToken,
	string RefreshToken,
	User User);