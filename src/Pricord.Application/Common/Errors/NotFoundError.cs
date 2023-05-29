using System.Net;
using Pricord.Domain.Common.Models;

namespace Pricord.Application.Common.Errors;

public sealed record NotFoundError : Error
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public NotFoundError() 
        : base("Not Found", "The requested resource was not found.")
    {
    }

    public NotFoundError(string message) 
        : base("Not Found", message)
    {
    }

    public NotFoundError(string title, string message) 
        : base(title, message)
    {
    }
}