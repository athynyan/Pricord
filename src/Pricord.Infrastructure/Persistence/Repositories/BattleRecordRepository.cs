using Microsoft.EntityFrameworkCore;
using Pricord.Application.BattleRecords.Contracts;
using Pricord.Application.BattleRecords.Persistence;
using Pricord.Domain.BattleRecords;
using Pricord.Domain.BattleRecords.ValueObjects;

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

    public Task<BattleRecordDetailsResult?> GetBattleRecordDetails(BattleRecordId id)
    {
        return _context.BattleRecords
            .AsNoTracking()
            .Where(r => r.Id == id)
            .Include(r => r.Boss)
            .Include(r => r.Timeline)
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