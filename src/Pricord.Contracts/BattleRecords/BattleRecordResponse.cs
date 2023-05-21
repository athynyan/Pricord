using Pricord.Contracts.Common.Abstractions;

namespace Pricord.Contracts.BattleRecords;

public sealed record BattleRecordResponse(
    Guid Id,
    long ExpectedDamage,
    string BattleType,
    Guid BossId,
    Guid[] PlayableCharacterIds,
    Guid? TimelineId) : IResponse;