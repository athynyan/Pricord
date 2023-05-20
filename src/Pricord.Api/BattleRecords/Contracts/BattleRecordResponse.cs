namespace Pricord.Api.BattleRecords.Contracts;

public sealed record BattleRecordResponse(
    Guid Id,
    long ExpectedDamage,
    string BattleType,
    Guid BossId,
    Guid[] PlayableCharacterIds,
    Guid? TimelineId);