using Pricord.Domain.Timelines;
using Pricord.Domain.Timelines.ValueObjects;

namespace Pricord.Application.Timelines.Persistence;

public interface ITimelineRepository
{
    Task AddAsync(Timeline timeline);
    Task<Timeline?> GetAsync(TimelineId id);
}