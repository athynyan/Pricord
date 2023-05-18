namespace Pricord.Application.Common.Settings;

public class DatabaseSettings
{
	public const string SectionName = "DatabaseSettings";
	public const string DefaultSchema = "application";
	public string Host { get; init; } = null!;
	public int Port { get; init; } = 5432;
	public string Username { get; init; } = null!;
	public string Password { get; init; } = null!;
	public string Database { get; init; } = null!;

	public string ConnectionString =>
		$"Server={Host};Port={Port};Database={Database};User Id={Username};Password={Password};";
}