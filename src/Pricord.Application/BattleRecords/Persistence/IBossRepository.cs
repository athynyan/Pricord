using Pricord.Domain.Units;

namespace Pricord.Application.BattleRecords.Persistence;

public interface IBossRepository
{
    Task AddAsync(Boss boss);
}