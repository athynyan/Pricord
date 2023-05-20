using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pricord.Api.Authentication.Mappers;
using Pricord.Application.Api.Contracts;
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

    [HttpPost(AuthenticationEndpoints.RegisterEndpoint)] // http://localhost:port/register
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
}