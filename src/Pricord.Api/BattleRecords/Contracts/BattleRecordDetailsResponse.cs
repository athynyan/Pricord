using Pricord.Application.BattleRecords.Contracts.Dtos;

namespace Pricord.Api.BattleRecords.Contracts;

public sealed record BattleRecordDetailsResponse(
    Guid Id,
    BossDto Boss,
    long ExpectedDamage,
    string BattleType,
    PlayableCharacterDto[] PlayableCharacters,
    TimelineDto? Timeline);