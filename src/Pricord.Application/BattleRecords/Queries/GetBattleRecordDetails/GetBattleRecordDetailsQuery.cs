using MediatR;
using Pricord.Application.BattleRecords.Models;
using Pricord.Domain.BattleRecords.ValueObjects;
using Pricord.Domain.Common.Models;

namespace Pricord.Application.BattleRecords.Queries.GetBattleRecord;

public sealed record GetBattleRecordDetailsQuery(BattleRecordId Id) 
    : IRequest<Result<BattleRecordDetailsResult>>;