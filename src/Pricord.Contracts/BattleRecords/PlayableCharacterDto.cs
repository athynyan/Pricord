namespace Pricord.Contracts.BattleRecords;

public sealed record PlayableCharacterDto(
    string PrefabId,
    int Level,
    int Rank,
    int Rarity);