using MediatR;
using Pricord.Application.BattleRecords.Models;
using Pricord.Domain.BattleRecords.ValueObjects;

namespace Pricord.Application.BattleRecords.Queries.GetBattleRecord;

public sealed record GetBattleRecordDetailsQuery(BattleRecordId Id) 
    : IRequest<BattleRecordDetailsResult>;