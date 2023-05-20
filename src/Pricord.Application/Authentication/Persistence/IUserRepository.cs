using Pricord.Domain.Authentication;
using Pricord.Domain.Authentication.ValueObjects;
using Pricord.Domain.Common.ValueObjects;

namespace Pricord.Application.Authentication.Persistence;

public interface IUserRepository
{
    Task<User?> FindByIdAsync(UserId id);
    Task<User?> FindByEmailAsync(Email email);
    Task<User?> FindByNameAsync(Name name);
    Task<User?> FindByRefreshToken(string refreshToken);

    Task AddAsync(User user);
}