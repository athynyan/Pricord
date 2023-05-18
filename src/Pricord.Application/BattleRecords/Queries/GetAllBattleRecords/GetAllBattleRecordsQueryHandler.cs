using MediatR;
using Pricord.Application.BattleRecords.Contracts;
using Pricord.Application.BattleRecords.Persistence;

namespace Pricord.Application.BattleRecords.Queries.GetAllBattleRecords;

public sealed class GetAllBattleRecordsQueryHandler : IRequestHandler<GetAllBattleRecordsQuery, IEnumerable<BattleRecordResult>>
{
    private readonly IBattleRecordRepository _battleRecordRepository;

    public GetAllBattleRecordsQueryHandler(IBattleRecordRepository battleRecordRepository)
    {
        _battleRecordRepository = battleRecordRepository;
    }

    public async Task<IEnumerable<BattleRecordResult>> Handle(GetAllBattleRecordsQuery request, CancellationToken cancellationToken)
    {
        return await _battleRecordRepository.GetAllAsync();
    }
}