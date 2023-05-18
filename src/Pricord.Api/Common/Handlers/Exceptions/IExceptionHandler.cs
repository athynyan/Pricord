namespace Pricord.Api.Common.Handlers.Exceptions;

public interface IExceptionHandler
{
    Task HandleAsync(HttpContext httpContext, Exception exception);
}