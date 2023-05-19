using MediatR;
using Pricord.Application.BattleRecords.Contracts;
using Pricord.Application.BattleRecords.Contracts.Dtos;
using Pricord.Application.Timelines.Contracts;

namespace Pricord.Application.BattleRecords.Commands.CreateBattleRecord;

public sealed record CreateBattleRecordCommand(
    BossDto Boss,
    PlayableCharacterDto[] PlayableCharacters,
    long ExpectedDamage,
    string BattleType,
    CreateTimelineDto? Timeline
) : IRequest<BattleRecordResult>;