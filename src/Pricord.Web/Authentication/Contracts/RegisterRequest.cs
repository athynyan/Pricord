namespace Pricord.Web.Authentication.Contracts;

public sealed record RegisterRequest(string Username, string Password, string? Email, string? role);