using System.Net;
using System.Text.Json;

namespace Pricord.Api.Common.Handlers.Exceptions;

public sealed class DefaultExceptionHandler : IExceptionHandler
{
    public async Task HandleAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json+problem";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var problemDetails = new
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Title = "An error occurred while processing your request.",
            Status = httpContext.Response.StatusCode,
            Instance = httpContext.Request.Path,
            Detail = exception.Message
        };

        var result = JsonSerializer.Serialize(problemDetails);

        await httpContext.Response.WriteAsync(result);
    }
}
