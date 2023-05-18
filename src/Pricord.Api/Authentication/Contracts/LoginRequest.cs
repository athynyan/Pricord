using System.ComponentModel.DataAnnotations;

namespace Pricord.Application.Api.Contracts;

public sealed record LoginRequest(
	[Required] string Username,
	[Required] string Password);