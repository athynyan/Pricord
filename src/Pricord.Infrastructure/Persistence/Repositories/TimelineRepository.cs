using Pricord.Application.BattleRecords.Persistence;
using Pricord.Domain.Timelines;

namespace Pricord.Infrastructure.Persistence.Repositories;

internal sealed class TimelineRepository : ITimelineRepository
{
    private readonly ApplicationDbContext _context;

    public TimelineRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Timeline timeline)
    {
        await _context.Timelines.AddAsync(timeline);
    }
}
