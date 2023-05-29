using System.Net;
using Pricord.Domain.Common.Models;

namespace Pricord.Application.Common.Errors;

public sealed record ValidationError : Error, IResponseError
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public Dictionary<string, string> Errors { get; init; } = new();

    public ValidationError(Dictionary<string, string> errors) : base("Validation error", "")
    {
        Errors = errors;
    }

}
