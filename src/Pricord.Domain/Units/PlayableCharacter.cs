using Pricord.Domain.Common.Models;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Domain.Units;

public sealed class PlayableCharacter : AggregateRoot<PlayableCharacterId, Guid>
{
    public PrefabId PrefabId { get; private set; } = default!;
    public Unit Unit { get; private set; } = default!;
    public Level Level { get; private set; } = default!;
    public Rank Rank { get; private set; } = default!;
    public Rarity Rarity { get; private set; } = default!;

    private PlayableCharacter() : base(PlayableCharacterId.Create())
    {
    }

    public static PlayableCharacter Create(PrefabId prefabId, Level level, Rank rank, Rarity rarity)
    {
        return new PlayableCharacter
        {
            PrefabId = prefabId,
            Level = level,
            Rank = rank,
            Rarity = rarity
        };
    }
}