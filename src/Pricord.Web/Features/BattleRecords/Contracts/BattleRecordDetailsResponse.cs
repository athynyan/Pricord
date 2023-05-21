using Pricord.Contracts.BattleRecords;
using Pricord.Contracts.Timelines;

namespace Pricord.Web.Features.BattleRecords.Contracts;

public sealed record BattleRecordDetailsResponse(
    Guid Id,
    BossDto Boss,
    long ExpectedDamage,
    string BattleType,
    PlayableCharacterDto[] PlayableCharacters,
    TimelineDto? Timeline);