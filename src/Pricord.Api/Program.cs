using Pricord.Api.Common.Middlewares;
using Pricord.Application;
using Pricord.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    var configuration = builder.Configuration;

    builder.Logging.ClearProviders();

    // Add services to the container.
    builder.Services
        .AddApplication()
        .AddInfrastructure(configuration);
    
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(
        o => o.WithOrigins(builder.Configuration["FrontendUrl"]!)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());

    app.UseMiddleware<ExceptionMiddleware>();

    // app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}

app.Run();
