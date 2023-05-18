using Pricord.Application.BattleRecords.Contracts.Dtos;

namespace Pricord.Api.BattleRecords.Contracts;

public sealed record CreateBattleRecordRequest(
    BossDto Boss,
    PlayableCharacterDto[] PlayableCharacters,
    int ExpectedDamage,
    string BattleType,
    TimelineDto? Timeline);