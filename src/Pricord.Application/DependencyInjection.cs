using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Pricord.Application.Common.Behaviors;
using Serilog;
using Serilog.Events;

namespace Pricord.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Information()
			.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
			.Enrich.FromLogContext()
			.WriteTo.Console()
			.CreateLogger();

		services.AddLogging(c =>
        {
			c.AddSerilog();
        });

		services.AddMediatR(c =>
			c.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		
		return services;
	}
}