namespace Pricord.Web.Features.Authentication.Contracts;

public sealed record RegisterRequest(string Name, string Password, string? Email, string role = "User");