namespace Pricord.Application.Authentication.Contracts;

public sealed record UserDto(
	Guid Id,
	string Name,
	string? Email,
	string Role,
	DateTime CreatedAt,
	DateTime? LastModified);