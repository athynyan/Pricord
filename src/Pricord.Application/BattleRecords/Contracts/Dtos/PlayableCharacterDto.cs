namespace Pricord.Application.BattleRecords.Contracts.Dtos;

public sealed record PlayableCharacterDto(
    string PrefabId,
    int Level,
    int Rank,
    int Rarity);