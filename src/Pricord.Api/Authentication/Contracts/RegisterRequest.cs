using System.ComponentModel.DataAnnotations;

namespace Pricord.Application.Api.Contracts;

public sealed record RegisterRequest(
    [Required] string Name,
    [Required] string Password,
    string? Email,
    string? Role);
