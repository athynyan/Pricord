using Microsoft.EntityFrameworkCore;
using Pricord.Application.BattleRecords.Persistence;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Infrastructure.Persistence.Repositories;

internal sealed class UnitRepository : IUnitRepository
{
    private readonly ApplicationDbContext _context;

    public UnitRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExistsAsync(PrefabId id)
    {
        return _context.Units.AnyAsync(u => u.Id == id);
    }
}
