using Microsoft.EntityFrameworkCore;
using Pricord.Application.Timelines.Persistence;
using Pricord.Domain.Timelines;
using Pricord.Domain.Timelines.ValueObjects;

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

    public Task<Timeline?> GetAsync(TimelineId id)
    {
        return _context.Timelines
            .AsNoTracking()
            .Where(tl => tl.Id == id)
            .Include(tl => tl.Items)
            .Include(tl => tl.Video)
            .SingleOrDefaultAsync();
    }
}
