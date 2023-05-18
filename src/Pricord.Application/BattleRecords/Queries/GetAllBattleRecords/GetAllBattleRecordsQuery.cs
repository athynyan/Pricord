using MediatR;
using Pricord.Application.BattleRecords.Contracts;

namespace Pricord.Application.BattleRecords.Queries.GetAllBattleRecords;

public sealed record GetAllBattleRecordsQuery : IRequest<IEnumerable<BattleRecordResult>>;