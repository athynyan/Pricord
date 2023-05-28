namespace Pricord.Application.Common.Settings;

public class DatabaseSettings
{
	public const string SectionName = "DatabaseSettings";
	public const string DefaultSchema = "application";
	public string Host { get; init; } = null!;
	public string? Port { get; init; } = null;
	public string Username { get; init; } = null!;
	public string Password { get; init; } = null!;
	public string Database { get; init; } = null!;

	public string ConnectionString =>
		Port is null
			? $"Server={Host};Database={Database};User Id={Username};Password={Password};"
			: $"Server={Host};Port={Port};Database={Database};User Id={Username};Password={Password};";
}