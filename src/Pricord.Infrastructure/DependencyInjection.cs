using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pricord.Application.Authentication.Persistence;
using Pricord.Application.BattleRecords.Persistence;
using Pricord.Application.Common.Persistence;
using Pricord.Application.Common.Services;
using Pricord.Application.Common.Settings;
using Pricord.Application.Timelines.Persistence;
using Pricord.Infrastructure.Persistence;
using Pricord.Infrastructure.Persistence.Repositories;
using Pricord.Infrastructure.Services;

namespace Pricord.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{   
        services.AddMemoryCache();

        services
            .AddPersistence(configuration)
            .AddJwtAuthentication(configuration);
        
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        return services;
	}


    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSettings = configuration.GetSection(DatabaseSettings.SectionName).Get<DatabaseSettings>();
        if (databaseSettings == null) throw new Exception("Database settings are not configured.");

        services.AddSingleton(Options.Create(databaseSettings));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(databaseSettings.ConnectionString));

        services.AddScoped<UserRepository>();
        services.AddScoped<IUserRepository, CachedUserRepository>();

        services.AddScoped<BattleRecordRepository>();
        services.AddScoped<IBattleRecordRepository, CachedBattleRecordRepository>();

        services.AddScoped<TimelineRepository>();
        services.AddScoped<ITimelineRepository, CachedTimelineRepository>();

        services.AddScoped<BossRepository>();
        services.AddScoped<IBossRepository, CachedBossRepository>();

        services.AddScoped<UnitRepository>();
        services.AddScoped<IUnitRepository, CachedUnitRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();
        if (jwtSettings is null) throw new ArgumentNullException(nameof(jwtSettings), "Authentication settings are not configured.");

        services.AddSingleton(Options.Create(jwtSettings));

        services.AddScoped<IJwtService, JwtService>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtSettings.Issuer,
					ValidAudience = jwtSettings.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
				};
			});

        return services;
    }
}