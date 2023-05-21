using MediatR;
using Pricord.Application.Authentication.Models;
using Pricord.Domain.Authentication.ValueObjects;
using Pricord.Domain.Common.ValueObjects;

namespace Pricord.Application.Authentication.Commands.Register;

public sealed record RegisterCommand(
    Name Name,
    string Password,
    Email? Email,
    string Role) : IRequest<AuthenticationResult>;