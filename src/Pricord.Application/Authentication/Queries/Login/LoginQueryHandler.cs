using System.Text.RegularExpressions;
using MediatR;
using Pricord.Application.Authentication.Models;
using Pricord.Application.Authentication.Exceptions;
using Pricord.Application.Authentication.Persistence;
using Pricord.Application.Common.Persistence;
using Pricord.Application.Common.Services;
using Pricord.Domain.Authentication;
using Pricord.Domain.Authentication.ValueObjects;
using Pricord.Domain.Common.ValueObjects;

namespace Pricord.Application.Authentication.Queries.Login;

internal sealed partial class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
{
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LoginQueryHandler(IJwtService jwtService, IPasswordHasher passwordHasher, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        User? existingUser = null;

        if (EmailRegex().IsMatch(request.Username))
        {
            existingUser = await _userRepository.FindByEmailAsync(Email.Create(request.Username));
        }
        else
        {
            existingUser = await _userRepository.FindByNameAsync(Name.Create(request.Username));
        }

        if (existingUser is null)
        {
            throw new InvalidCredentialsException();
        }

        if (!_passwordHasher.VerifyPassword(request.Password, existingUser.Password))
        {
            throw new InvalidCredentialsException();
        }

        var accessToken = _jwtService.GenerateAccessToken(existingUser);
        var refreshToken = _jwtService.GenerateRefreshToken();

        existingUser.AddToken(refreshToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthenticationResult(
            accessToken,
            refreshToken.Value,
            existingUser);
    }

    [GeneratedRegex(@"/^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})$/")]
    private static partial Regex EmailRegex();
}