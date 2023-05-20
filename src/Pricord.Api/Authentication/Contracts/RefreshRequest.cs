using System.ComponentModel.DataAnnotations;

namespace Pricord.Api.Authentication.Contracts;

public sealed record RefreshRequest([Required]string RefreshToken);