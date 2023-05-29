using MediatR;
using Pricord.Application.Authentication.Models;
using Pricord.Application.Authentication.Exceptions;
using Pricord.Application.Authentication.Persistence;
using Pricord.Application.Common.Persistence;
using Pricord.Application.Common.Services;
using Pricord.Domain.Authentication;
using Pricord.Domain.Authentication.Enums;
using Pricord.Domain.Common.Models;

namespace Pricord.Application.Authentication.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthenticationResult>>
{
    
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public RegisterCommandHandler(
        IPasswordHasher passwordHasher,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IJwtService jwtService)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<Result<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.FindByNameAsync(request.Name);

        if (existingUser is not null)
        {
            return new UserExistError();
        }

        var hashedPassword = _passwordHasher.HashPassword(request.Password);

        var createdUser = User.Create(
            request.Name,
            hashedPassword,
            Enum.Parse<Role>(request.Role, true),
            request.Email);

        var accessToken = _jwtService.GenerateAccessToken(createdUser);
        var refreshToken = _jwtService.GenerateRefreshToken();

        createdUser.AddToken(refreshToken);
        
        await _userRepository.AddAsync(createdUser);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthenticationResult(
            accessToken,
            refreshToken.Value,
            createdUser);
    }
}