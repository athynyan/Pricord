using Pricord.Domain.Timelines;

namespace Pricord.Application.BattleRecords.Persistence;

public interface ITimelineRepository
{
    Task AddAsync(Timeline timeline);
}