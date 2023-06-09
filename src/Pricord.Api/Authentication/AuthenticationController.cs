using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pricord.Api.Authentication.Mappers;
using Pricord.Application.Authentication.Models;
using Pricord.Application.Authentication.Queries.Refresh;
using Pricord.Application.Common.Errors;
using Pricord.Contracts.Authentication;
using Pricord.Contracts.Common.Constants;
using Pricord.Domain.Common.Models;

namespace Pricord.Api.Authentication;

[ApiController]
public sealed class AuthenticationController : ControllerBase
{
    private readonly ISender _sender;

    public AuthenticationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost(AuthenticationEndpoints.RegisterEndpoint)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _sender.Send(request.ToCommand());
        return FormatResult(result);
    }

    [HttpPost(AuthenticationEndpoints.LoginEndpoint)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _sender.Send(request.ToQuery());
        return FormatResult(result);
    }

    [HttpPost(AuthenticationEndpoints.RefreshEndpoint)]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        var result = await _sender.Send(new RefreshTokenQuery(request.RefreshToken));
        return FormatResult(result);
    }

    private IActionResult FormatResult(Result<AuthenticationResult> result)
    {
        return result.Match<IActionResult>(
            success => Ok(success),
            error => FormatErrors(error));
    }

    private IActionResult FormatErrors(Error error)
    {
        if (error is ValidationError validationError)
        {
            var modelState = validationError.Errors.Aggregate(new ModelStateDictionary(), (dictionary, error) => {
                dictionary.AddModelError(error.Key, error.Value);
                return dictionary;
            });

            return ValidationProblem(modelState);
        }

        return Problem(statusCode: (int)((IResponseError)error).StatusCode, title: error.Title, detail: error.Message);
    }
}