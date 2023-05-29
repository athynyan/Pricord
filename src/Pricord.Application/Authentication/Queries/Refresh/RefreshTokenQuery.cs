using MediatR;
using Pricord.Application.Authentication.Models;
using Pricord.Domain.Common.Models;

namespace Pricord.Application.Authentication.Queries.Refresh;

public sealed record RefreshTokenQuery(string RefreshToken) : IRequest<Result<AuthenticationResult>>;