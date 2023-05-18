using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pricord.Api.Authentication.Mappers;
using Pricord.Application.Api.Contracts;

namespace Pricord.Api.Authentication;

[ApiController]
public sealed class AuthenticationController : ControllerBase
{
    private readonly ISender _sender;

    public AuthenticationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")] // http://localhost:port/register
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _sender.Send(request.ToCommand());
        return Ok(result.ToResponse());
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _sender.Send(request.ToQuery());
        return Ok(result.ToResponse());
    }
}