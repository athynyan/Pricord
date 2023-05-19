using Pricord.Application.BattleRecords.Contracts.Dtos;
using Pricord.Application.Timelines.Contracts;

namespace Pricord.Api.BattleRecords.Contracts;

public sealed record CreateBattleRecordRequest(
    BossDto Boss,
    PlayableCharacterDto[] PlayableCharacters,
    int ExpectedDamage,
    string BattleType,
    CreateTimelineDto? Timeline);