using System.Net;

namespace Pricord.Application.Common.Errors;

public interface IResponseError
{
    HttpStatusCode StatusCode { get; }
}