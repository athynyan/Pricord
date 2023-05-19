using Microsoft.Extensions.Caching.Memory;
using Pricord.Application.Authentication.Persistence;
using Pricord.Application.Common.Services;
using Pricord.Domain.Authentication;
using Pricord.Domain.Authentication.ValueObjects;
using Pricord.Domain.Common.ValueObjects;

namespace Pricord.Infrastructure.Persistence.Repositories;

internal sealed class CachedUserRepository : IUserRepository
{
    private readonly UserRepository _userRepository;
    private readonly IMemoryCache _cache;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly string _key = "Unit_";
    private readonly int _expirationMinutes = 5;

    public CachedUserRepository(
        UserRepository userRepository,
        IMemoryCache cache,
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _cache = cache;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task AddAsync(User user)
    {
        await _userRepository.AddAsync(user);
    }

    public Task<User?> FindByEmailAsync(Email email)
    {
        return _cache.GetOrCreateAsync(_key + email, async entry =>
        {
            entry.AbsoluteExpiration = _dateTimeProvider.UtcNow.AddMinutes(_expirationMinutes);
            return await _userRepository.FindByEmailAsync(email);
        });
    }

    public Task<User?> FindByIdAsync(UserId id)
    {
        return _cache.GetOrCreateAsync(_key + id, async entry =>
        {
            entry.AbsoluteExpiration = _dateTimeProvider.UtcNow.AddMinutes(_expirationMinutes);
            return await _userRepository.FindByIdAsync(id);
        });
    }

    public Task<User?> FindByNameAsync(Name name)
    {
        return _cache.GetOrCreateAsync(_key + name, async entry =>
        {
            entry.AbsoluteExpiration = _dateTimeProvider.UtcNow.AddMinutes(_expirationMinutes);
            return await _userRepository.FindByNameAsync(name);
        });
    }
}