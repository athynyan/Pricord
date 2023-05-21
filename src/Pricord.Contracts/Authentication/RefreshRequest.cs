using System.ComponentModel.DataAnnotations;
using Pricord.Contracts.Common.Abstractions;

namespace Pricord.Contracts.Authentication;

public sealed record RefreshRequest([Required]string RefreshToken) : IRequest<AuthenticationResponse>;