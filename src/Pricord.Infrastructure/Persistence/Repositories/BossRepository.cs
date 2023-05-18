using Pricord.Application.BattleRecords.Persistence;
using Pricord.Domain.Units;

namespace Pricord.Infrastructure.Persistence.Repositories;

internal sealed class BossRepository : IBossRepository
{
    private readonly ApplicationDbContext _context;

    public BossRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Boss boss)
    {
        await _context.Boss.AddAsync(boss);
    }
}
