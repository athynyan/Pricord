using Pricord.Contracts.Common.Abstractions;
using Pricord.Contracts.Timelines;

namespace Pricord.Contracts.BattleRecords;

public sealed record BattleRecordDetailsResponse(
    Guid Id,
    BossDto Boss,
    long ExpectedDamage,
    string BattleType,
    PlayableCharacterDto[] PlayableCharacters,
    TimelineDto? Timeline) : IResponse;