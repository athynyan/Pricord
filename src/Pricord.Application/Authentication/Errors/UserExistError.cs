using System.Net;
using Pricord.Domain.Common.Models;

namespace Pricord.Application.Authentication.Exceptions;

public sealed record UserExistError : Error
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public UserExistError() : base("Conflict", "The user already exists.")
    {
    }
}