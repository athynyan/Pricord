using MediatR;
using Pricord.Application.Authentication.Models;

namespace Pricord.Application.Authentication.Queries.Login;

public sealed record LoginQuery(
    string Username,
    string Password) : IRequest<AuthenticationResult>;