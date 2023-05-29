using System.Net;
using Pricord.Application.Common.Errors;
using Pricord.Domain.Common.Models;

namespace Pricord.Application.Authentication.Exceptions;

public sealed record UserExistError : Error, IResponseError
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public UserExistError() : base("Conflict", "The user already exists.")
    {
    }
}