using Pricord.Application.Timelines.Contracts.Dtos;
using Pricord.Domain.Timelines;
using Pricord.Domain.Timelines.ValueObjects;

namespace Pricord.Application.Timelines.Persistence;

public interface ITimelineRepository
{
    Task AddAsync(Timeline timeline);
    Task<TimelineDto?> GetAsync(TimelineId id);
}