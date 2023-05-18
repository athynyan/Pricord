namespace Pricord.Api.BattleRecords.Contracts;

public sealed record CreateBattleRecordResponse(
    Guid Id,
    Guid BossId,
    Guid? TimelineId,
    Guid[] PlayableCharacterIds);