using MediatR;
using Pricord.Application.BattleRecords.Contracts;
using Pricord.Domain.BattleRecords.ValueObjects;

namespace Pricord.Application.BattleRecords.Queries.GetBattleRecord;

public sealed record GetBattleRecordDetailsQuery(BattleRecordId Id) 
    : IRequest<BattleRecordDetailsResult>;