using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Application.BattleRecords.Persistence;

public interface IUnitRepository
{
    Task<bool> ExistsAsync(PrefabId id);
}