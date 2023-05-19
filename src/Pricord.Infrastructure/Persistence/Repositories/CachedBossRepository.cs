using Microsoft.Extensions.Caching.Memory;
using Pricord.Application.BattleRecords.Persistence;
using Pricord.Application.Common.Services;
using Pricord.Domain.Units;

namespace Pricord.Infrastructure.Persistence.Repositories;

internal sealed class CachedBossRepository : IBossRepository
{
    private readonly BossRepository _repository;
    private readonly IMemoryCache _cache;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly string _key = "Boss_";

    public CachedBossRepository(
        BossRepository repository,
        IMemoryCache cache,
        IDateTimeProvider dateTimeProvider)
    {
        _repository = repository;
        _cache = cache;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task AddAsync(Boss boss)
    {
        await _repository.AddAsync(boss);
    }
}