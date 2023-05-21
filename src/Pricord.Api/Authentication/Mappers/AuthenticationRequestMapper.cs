using Pricord.Application.Authentication.Commands.Register;
using Pricord.Application.Authentication.Queries.Login;
using Pricord.Contracts.Authentication;
using Pricord.Domain.Authentication.Enums;
using Pricord.Domain.Authentication.ValueObjects;
using Pricord.Domain.Common.ValueObjects;

namespace Pricord.Api.Authentication.Mappers;

internal static class AuthenticationRequestMapper
{
    internal static RegisterCommand ToCommand(this RegisterRequest request)
        => new RegisterCommand(
            Name.Create(request.Name), 
            request.Password, 
            request.Email is not null ? Email.Create(request.Email) : null, 
            request.Role ?? Role.User.ToString());

    internal static LoginQuery ToQuery(this LoginRequest request)
        => new LoginQuery(request.Username, request.Password);
}