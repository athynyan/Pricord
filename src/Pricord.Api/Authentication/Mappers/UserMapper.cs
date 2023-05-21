using Pricord.Contracts.Authentication;
using Pricord.Domain.Authentication;

namespace Pricord.Application.Authentication.Mappers;

internal static class UserMapper
{
    internal static UserDto ToDto(this User user) 
        => new UserDto(user.Id.Value,
            user.Name.Value,
            user.Email?.Value,
            user.Role.ToString(),
            user.CreatedAt,
            user.LastModified);
}