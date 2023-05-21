using Microsoft.EntityFrameworkCore;
using Pricord.Application.BattleRecords.Models;
using Pricord.Application.BattleRecords.Persistence;
using Pricord.Domain.BattleRecords;
using Pricord.Domain.BattleRecords.ValueObjects;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Infrastructure.Persistence.Repositories;

internal sealed class BattleRecordRepository : IBattleRecordRepository
{
    private readonly ApplicationDbContext _context;

    public BattleRecordRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(BattleRecord record)
    {
        await _context.BattleRecords.AddAsync(record);
    }

    public async Task<IEnumerable<BattleRecordResult>> GetAllAsync()
    {
        return await _context.BattleRecords
            .AsNoTracking()
            .Select(br => new BattleRecordResult(
                br.Id,
                br.BossId,
                br.ExpectedDamage,
                br.TimelineId,
                br.PlayableCharacters
                    .Select(pc => pc.Id)
                    .ToArray()
                ))
            .ToArrayAsync();
    }

    public Task<BattleRecordDetailsResult?> GetBattleRecordDetails(BattleRecordId id)
    {
        return _context.BattleRecords
            .AsNoTracking()
            .Where(r => r.Id == id)
            .Include(r => r.Boss)
            .Include(r => r.Timeline)
            .ThenInclude(t => t!.Items)
            .Include(r => r.PlayableCharacters)
            .Select(r => new BattleRecordDetailsResult(
                r.Id,
                r.Boss,
                r.ExpectedDamage,
                r.Timeline,
                r.PlayableCharacters.ToArray()))
            .SingleOrDefaultAsync();
    }
}