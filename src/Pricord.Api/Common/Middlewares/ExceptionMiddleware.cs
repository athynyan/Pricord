using System.Net;
using System.Text.Json;
using FluentValidation;
using Pricord.Api.Common.Handlers.Exceptions;
using Pricord.Application.Authentication.Exceptions;

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
        catch (ValidationException e)
        {
            await HandleExceptionAsync(httpContext, e, new ValidationExceptionHandler());
        }
        catch (InvalidCredentialsException e)
        {
            await HandleExceptionAsync(httpContext, e, new InvalidCredentialsExceptionHandler());
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(httpContext, e, new DefaultExceptionHandler());
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception e, IExceptionHandler handler)
    {
        await handler.HandleAsync(httpContext, e);
    }
}
