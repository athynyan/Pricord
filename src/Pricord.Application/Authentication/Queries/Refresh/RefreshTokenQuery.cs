using MediatR;
using Pricord.Application.Authentication.Models;

namespace Pricord.Application.Authentication.Queries.Refresh;

public sealed record RefreshTokenQuery(string RefreshToken) : IRequest<AuthenticationResult>;