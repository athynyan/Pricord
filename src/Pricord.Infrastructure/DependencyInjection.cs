using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<BattleRecordRepository>();
        services.AddScoped<IBattleRecordRepository, CachedBattleRecordRepository>();

        services.AddScoped<TimelineRepository>();
        services.AddScoped<ITimelineRepository, CachedTimelineRepository>();

        services.AddScoped<IBossRepository, BossRepository>();

        services.AddScoped<IUnitRepository, UnitRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();
        if (jwtSettings is null) throw new ArgumentNullException(nameof(jwtSettings), "Authentication settings are not configured.");

        services.AddSingleton(Options.Create(jwtSettings));

        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}