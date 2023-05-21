using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pricord.Application.Common.Services;
using Pricord.Application.Common.Settings;
using Pricord.Domain.Authentication;

namespace Pricord.Infrastructure.Services;

internal sealed class JwtService : IJwtService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;

    public JwtService(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }

    public string GenerateAccessToken(User user)
    {
        var signingCredentials = new SigningCredentials(
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
			SecurityAlgorithms.HmacSha256);

		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new Claim(ClaimTypes.NameIdentifier, user.Id.Value.ToString()),
			new Claim(ClaimTypes.Name, user.Name.Value),
			new Claim(ClaimTypes.Role, user.Role.ToString())
		};

		var token = new JwtSecurityToken(
			_jwtSettings.Issuer,
			_jwtSettings.Audience,
			claims,
			expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
			signingCredentials: signingCredentials);

		return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public Token GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(randomNumber);

        return Token.Create(
            Convert.ToBase64String(randomNumber), 
            _dateTimeProvider.UtcNow.AddDays(_jwtSettings.RefreshExpiryDays));
    }

    public bool ValidateRefreshToken(string tokenString, Token token)
    {
        return tokenString == token.Value && token.Expiry > _dateTimeProvider.UtcNow;
    }
}
