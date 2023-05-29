using System.Net;
using System.Text.Json;

namespace Pricord.Api.Common.Middlewares;

internal sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;    
    }
    
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(httpContext, e);
        }
    }

    private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
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

        return httpContext.Response.WriteAsync(result);
    }
}
