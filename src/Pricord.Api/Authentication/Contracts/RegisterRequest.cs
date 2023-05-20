using System.ComponentModel.DataAnnotations;

namespace Pricord.Api.Authentication.Contracts;

public sealed record RegisterRequest(
    [Required] string Name,
    [Required] string Password,
    string? Email,
    string? Role);
