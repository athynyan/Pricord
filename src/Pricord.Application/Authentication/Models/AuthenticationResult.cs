using Pricord.Domain.Authentication;

namespace Pricord.Application.Authentication.Models;

public sealed record AuthenticationResult(
	string AccessToken,
	string RefreshToken,
	User User);