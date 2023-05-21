namespace Pricord.Contracts.BattleRecords;

public sealed record BossDto(
    string PrefabId,
    int Level,
    long Health,
    int Tier);