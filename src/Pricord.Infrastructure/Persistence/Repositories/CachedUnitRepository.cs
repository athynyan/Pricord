using Microsoft.Extensions.Caching.Memory;
using Pricord.Application.BattleRecords.Persistence;
using Pricord.Application.Common.Services;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Infrastructure.Persistence.Repositories;

internal sealed class CachedUnitRepository : IUnitRepository
{
    private readonly UnitRepository _repository;
    private readonly IMemoryCache _cache;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly string _key = "Unit_";

    public CachedUnitRepository(
        UnitRepository repository,
        IMemoryCache cache,
        IDateTimeProvider dateTimeProvider)
    {
        _repository = repository;
        _cache = cache;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<bool> ExistsAsync(PrefabId id)
    {
        return await _cache.GetOrCreateAsync(_key + id, async entry =>
        {
            entry.AbsoluteExpiration = _dateTimeProvider.UtcNow.AddMinutes(5);
            return await _repository.ExistsAsync(id);
        });
    }
}
