using MediatR;
using Microsoft.Extensions.Options;
using Pricord.Application.Authentication.Models;
using Pricord.Application.Authentication.Exceptions;
using Pricord.Application.Authentication.Persistence;
using Pricord.Application.Common.Persistence;
using Pricord.Application.Common.Services;
using Pricord.Application.Common.Settings;
using Pricord.Domain.Common.Models;

namespace Pricord.Application.Authentication.Queries.Refresh;

internal sealed class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, Result<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenQueryHandler(
        IUserRepository userRepository,
        IJwtService jwtService,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        IOptions<JwtSettings> jwtSettings)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Result<AuthenticationResult>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByRefreshToken(request.RefreshToken);

        if (user is null)
            return new InvalidCredentialsError("Invalid refresh token");

        var accessToken = _jwtService.GenerateAccessToken(user);

        user.Tokens.FirstOrDefault(t => t.Value == request.RefreshToken)?
            .UpdateExpiration(_dateTimeProvider.UtcNow.AddDays(_jwtSettings.RefreshExpiryDays));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthenticationResult(
            accessToken,
            request.RefreshToken,
            user);
    }

}
