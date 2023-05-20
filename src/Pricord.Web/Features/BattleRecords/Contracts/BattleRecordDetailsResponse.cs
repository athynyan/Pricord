using Pricord.Application.BattleRecords.Contracts.Dtos;
using Pricord.Application.Timelines.Contracts.Dtos;

namespace Pricord.Web.Features.BattleRecords.Contracts;

public sealed record BattleRecordDetailsResponse(
    Guid Id,
    BossDto Boss,
    long ExpectedDamage,
    string BattleType,
    PlayableCharacterDto[] PlayableCharacters,
    TimelineDto? Timeline);