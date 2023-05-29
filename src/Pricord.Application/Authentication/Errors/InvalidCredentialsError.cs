using System.Net;
using Pricord.Domain.Common.Models;

namespace Pricord.Application.Authentication.Exceptions;

public sealed record InvalidCredentialsError : Error
{
    public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

    public InvalidCredentialsError() : base("Unauthorized", "Invalid username or password.")
    {
    }

    public InvalidCredentialsError(string message) : base("Unauthorized", message)
    {
    }
}