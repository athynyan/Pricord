using System.ComponentModel.DataAnnotations;
using Pricord.Contracts.Common.Abstractions;

namespace Pricord.Contracts.Authentication;

public sealed record RegisterRequest(
    [Required] string Name,
    [Required] string Password,
    string? Email,
    string? Role = "User") : IRequest<AuthenticationResponse>;
