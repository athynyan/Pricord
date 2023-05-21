using MediatR;
using Pricord.Application.BattleRecords.Models;
using Pricord.Application.Timelines.Models;
using Pricord.Domain.Units;

namespace Pricord.Application.BattleRecords.Commands.CreateBattleRecord;

public sealed record CreateBattleRecordCommand(
    Boss Boss,
    PlayableCharacter[] PlayableCharacters,
    long ExpectedDamage,
    string BattleType,
    CreateTimelineModel? Timeline
) : IRequest<BattleRecordResult>;