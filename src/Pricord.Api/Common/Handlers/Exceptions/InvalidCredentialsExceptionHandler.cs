using System.Text.Json;

namespace Pricord.Api.Common.Handlers.Exceptions;

public sealed class InvalidCredentialsExceptionHandler : IExceptionHandler
{
    public Task HandleAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json+problem";
        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        
        var problemDetails = new
        {
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
            Title = exception.Message,
            Status = httpContext.Response.StatusCode,
            Instance = httpContext.Request.Path,
        };

        var result = JsonSerializer.Serialize(problemDetails);

        return httpContext.Response.WriteAsync(result);
    }
}
