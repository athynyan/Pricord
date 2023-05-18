using Pricord.Application.BattleRecords.Contracts;
using Pricord.Domain.BattleRecords;
using Pricord.Domain.BattleRecords.ValueObjects;

namespace Pricord.Application.BattleRecords.Persistence;

public interface IBattleRecordRepository
{
    Task<BattleRecordDetailsResult?> GetBattleRecordDetails(BattleRecordId id);

    Task AddAsync(BattleRecord record);
}