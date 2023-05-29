using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pricord.Api.Authentication.Mappers;
using Pricord.Application.Authentication.Models;
using Pricord.Application.Authentication.Queries.Refresh;
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
            error => ValidationProblem(FormatErrors(error)));
    }

    private ModelStateDictionary FormatErrors(Error error)
    {
        var modelState = new ModelStateDictionary();
        modelState.AddModelError(error.Title, error.Message);
        return modelState;
    }
}