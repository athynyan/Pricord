using MediatR;
using Pricord.Application.BattleRecords.Contracts;
using Pricord.Application.BattleRecords.Persistence;
using Pricord.Application.BattleRecords.Queries.GetBattleRecord;
using Pricord.Application.Common.Exceptions;

namespace Pricord.Application.BattleRecords.Queries.GetBattleRecordDetails;

public sealed class GetBattleRecordDetailsQueryHandler : IRequestHandler<GetBattleRecordDetailsQuery, BattleRecordDetailsResult>
{
    private readonly IBattleRecordRepository _battleRecordRepository;

    public GetBattleRecordDetailsQueryHandler(IBattleRecordRepository battleRecordRepository)
    {
        _battleRecordRepository = battleRecordRepository;
    }

    public Task<BattleRecordDetailsResult> Handle(GetBattleRecordDetailsQuery request, CancellationToken cancellationToken)
    {
        var battleRecordDetails = _battleRecordRepository.GetBattleRecordDetails(request.Id);

        if (battleRecordDetails is null)
        {
            throw new NotFoundException($"Battle record with id {request.Id} not found.");
        }

        return battleRecordDetails!;
    }
}
