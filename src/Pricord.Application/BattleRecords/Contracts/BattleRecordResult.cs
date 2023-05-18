using Pricord.Domain.BattleRecords.ValueObjects;
using Pricord.Domain.Timelines.ValueObjects;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Application.BattleRecords.Contracts;

public sealed record BattleRecordResult(
    BattleRecordId Id,
    BossId BossId,
    Damage ExpectedDamage,
    TimelineId? TimelineId,
    PlayableCharacterId[] PlayableCharacterIds);