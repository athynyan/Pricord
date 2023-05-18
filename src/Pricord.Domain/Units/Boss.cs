using Pricord.Domain.Common.Models;
using Pricord.Domain.Units.Enums;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Domain.Units;

public sealed class Boss : AggregateRoot<BossId, Guid>
{
    public PrefabId PrefabId { get; private set; } = default!;
    public Unit Unit { get; private set; } = default!;
    public Level Level { get; private set; } = default!;
    public Health Health { get; private set; } = default!;
	public Tier Tier { get; private set; } = default!;

    private Boss() : base(BossId.Create())
    {
    }

    public static Boss Create(PrefabId prefabId, Level level, Health health, Tier tier)
    {
        return new Boss
        {
            PrefabId = prefabId,
            Level = level,
            Health = health,
            Tier = tier
        };
    }
}