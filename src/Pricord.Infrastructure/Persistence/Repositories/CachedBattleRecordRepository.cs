using Microsoft.Extensions.Caching.Memory;
using Pricord.Application.BattleRecords.Contracts;
using Pricord.Application.BattleRecords.Persistence;
using Pricord.Application.Common.Services;
using Pricord.Domain.BattleRecords;
using Pricord.Domain.BattleRecords.ValueObjects;

namespace Pricord.Infrastructure.Persistence.Repositories;

internal sealed class CachedBattleRecordRepository : IBattleRecordRepository
{
    private readonly BattleRecordRepository _repository;
    private readonly IMemoryCache _cache;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly string _key = "BattleRecords_";
    private readonly int _expirationMinutes = 5;

    public CachedBattleRecordRepository(
        BattleRecordRepository repository,
        IMemoryCache cache,
        IDateTimeProvider dateTimeProvider)
    {
        _repository = repository;
        _cache = cache;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task AddAsync(BattleRecord record)
    {
        await _repository.AddAsync(record);
    }

    public async Task<IEnumerable<BattleRecordResult>> GetAllAsync()
    {

        return await _cache.GetOrCreateAsync(_key, async entry =>
        {
            entry.AbsoluteExpiration = _dateTimeProvider.UtcNow.AddMinutes(_expirationMinutes);
            return await _repository.GetAllAsync();
        }) ?? Enumerable.Empty<BattleRecordResult>();
    }

    public async Task<BattleRecordDetailsResult?> GetBattleRecordDetails(BattleRecordId id)
    {
        return await _cache.GetOrCreateAsync(_key + id, async entry =>
        {
            entry.AbsoluteExpiration = _dateTimeProvider.UtcNow.AddMinutes(_expirationMinutes);
            return await _repository.GetBattleRecordDetails(id);
        });
    }
}
