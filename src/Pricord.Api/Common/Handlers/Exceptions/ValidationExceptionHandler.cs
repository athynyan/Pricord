using System.Net;
using System.Text.Json;
using FluentValidation;

namespace Pricord.Api.Common.Handlers.Exceptions;

public sealed class ValidationExceptionHandler : IExceptionHandler
{
    public Task HandleAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json+problem";
        httpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;

        var validationException = (ValidationException) exception;

        var errors = validationException.Errors.Select(e => 
            new { Property = e.PropertyName.Split(".")[0], Message = e.ErrorMessage });
        
        var problemDetails = new
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "One or more validation errors occurred.",
            Status = httpContext.Response.StatusCode,
            Instance = httpContext.Request.Path,
            Errors = errors
        };

        var result = JsonSerializer.Serialize(problemDetails);

        return httpContext.Response.WriteAsync(result);
    }
}
