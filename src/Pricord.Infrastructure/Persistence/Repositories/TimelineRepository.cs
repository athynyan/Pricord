using Microsoft.EntityFrameworkCore;
using Pricord.Application.Timelines.Contracts.Dtos;
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

    public Task<TimelineDto?> GetAsync(TimelineId id)
    {
        return _context.Timelines
            .AsNoTracking()
            .Where(tl => tl.Id == id)
            .Include(tl => tl.Items)
            .Include(tl => tl.Video)
            .Select(tl => 
                new TimelineDto(
                    tl.Id.Value,
                    tl.Items.Select(tli => new TimelineItemDto(
                            tli.Time, 
                            tli.AttackerId.Value, 
                            tli.ActionType.ToString(), 
                            tli.AdditionalInfo)), 
                    new VideoDto(
                        tl.Video!.Url, 
                        tl.Video.Type.ToString())))
            .SingleOrDefaultAsync();
    }
}
