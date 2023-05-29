using MediatR;
using Pricord.Application.BattleRecords.Models;
using Pricord.Application.BattleRecords.Persistence;
using Pricord.Application.BattleRecords.Queries.GetBattleRecord;
using Pricord.Application.Common.Errors;
using Pricord.Domain.Common.Models;

namespace Pricord.Application.BattleRecords.Queries.GetBattleRecordDetails;

public sealed class GetBattleRecordDetailsQueryHandler : IRequestHandler<GetBattleRecordDetailsQuery, Result<BattleRecordDetailsResult>>
{
    private readonly IBattleRecordRepository _battleRecordRepository;

    public GetBattleRecordDetailsQueryHandler(IBattleRecordRepository battleRecordRepository)
    {
        _battleRecordRepository = battleRecordRepository;
    }

    public async Task<Result<BattleRecordDetailsResult>> Handle(GetBattleRecordDetailsQuery request, CancellationToken cancellationToken)
    {
        var battleRecordDetails = await _battleRecordRepository.GetBattleRecordDetails(request.Id);

        if (battleRecordDetails is null)
        {
            return new NotFoundError($"Battle record with id {request.Id} not found.");
        }

        return battleRecordDetails!;
    }
}
