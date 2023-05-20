using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pricord.Api.Authentication.Contracts;
using Pricord.Api.Authentication.Mappers;
using Pricord.Application.Authentication.Queries.Refresh;
using Pricord.Application.Common.Constants;

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
        return Ok(result.ToResponse());
    }

    [HttpPost(AuthenticationEndpoints.LoginEndpoint)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _sender.Send(request.ToQuery());
        return Ok(result.ToResponse());
    }

    [HttpPost(AuthenticationEndpoints.RefreshEndpoint)]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        var result = await _sender.Send(new RefreshTokenQuery(request.RefreshToken));
        return Ok(result.ToResponse());
    }
}