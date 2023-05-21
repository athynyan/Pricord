using Pricord.Contracts.BattleRecords;

namespace Pricord.Web.Features.BattleRecords.Services;

public interface IBattleRecordService
{
    public Task<IEnumerable<BattleRecordResponse>> GetAllAsync();
    public Task<BattleRecordDetailsResponse?> GetBattleRecordDetailsAsync(Guid id);
}