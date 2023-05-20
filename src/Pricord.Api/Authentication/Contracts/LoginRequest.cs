using System.ComponentModel.DataAnnotations;

namespace Pricord.Api.Authentication.Contracts;

public sealed record LoginRequest(
	[Required] string Username,
	[Required] string Password);