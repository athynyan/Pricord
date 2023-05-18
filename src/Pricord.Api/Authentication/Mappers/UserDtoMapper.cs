using Pricord.Application.Api.Contracts.Dtos;
using Pricord.Domain.Authentication;

namespace Pricord.Api.Authentication.Mappers;

internal static class UserDtoMapper
{
    internal static UserDto ToDto(this User user) 
        => new UserDto(user.Id.Value,
            user.Name.Value,
            user.Email?.Value,
            user.Role.ToString(),
            user.CreatedAt,
            user.LastModified);
}