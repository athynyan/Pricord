using Microsoft.Extensions.Caching.Memory;
using Pricord.Application.Common.Services;
using Pricord.Application.Timelines.Persistence;
using Pricord.Domain.Timelines;
using Pricord.Domain.Timelines.ValueObjects;

namespace Pricord.Infrastructure.Persistence.Repositories;

internal sealed class CachedTimelineRepository : ITimelineRepository
{
    private readonly TimelineRepository _repository;
    private readonly IMemoryCache _cache;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly string _key = "Timelines_";

    public CachedTimelineRepository(
        TimelineRepository repository,
        IMemoryCache cache,
        IDateTimeProvider dateTimeProvider)
    {
        _repository = repository;
        _cache = cache;
        _dateTimeProvider = dateTimeProvider;
    }

    public Task AddAsync(Timeline timeline)
    {
        return _repository.AddAsync(timeline);
    }

    public Task<Timeline?> GetAsync(TimelineId id)
    {
        return _cache.GetOrCreateAsync(_key + id, async entry =>
        {
            entry.AbsoluteExpiration = _dateTimeProvider.UtcNow.AddMinutes(5);
            return await _repository.GetAsync(id);
        });
    }
}
