namespace Pricord.Application.BattleRecords.Contracts.Dtos;

public sealed record BossDto(
    string PrefabId,
    int Level,
    long Health,
    int Tier);