using System.ComponentModel.DataAnnotations;
using Pricord.Contracts.Common.Abstractions;

namespace Pricord.Contracts.Authentication;

public sealed record LoginRequest(
	[Required] string Username,
	[Required] string Password) : IRequest<AuthenticationResponse>;