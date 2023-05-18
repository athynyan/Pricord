using Pricord.Application.BattleRecords.Contracts.Dtos;
using Pricord.Domain.Units;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Api.BattleRecords.Mappers;

internal static class PlayableCharacterMapper
{
    public static PlayableCharacter ToEntity(this PlayableCharacterDto dto)
    {
        return PlayableCharacter.Create(
            PrefabId.Create(dto.PrefabId),
            Level.Create(dto.Level),
            Rank.Create(dto.Rank),
            Rarity.Create(dto.Rarity));
    }

    public static PlayableCharacterDto ToDto(this PlayableCharacter character)
    {
        return new PlayableCharacterDto(
            character.PrefabId.Value,
            character.Level.Value,
            character.Rank.Value,
            character.Rarity.Value);
    }
}