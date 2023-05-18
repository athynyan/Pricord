using Pricord.Domain.Common.Models;
using Pricord.Domain.Common.ValueObjects;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Domain.Units;

public sealed class Unit : Entity<PrefabId, string>
{
    public Name Name { get; private set; } = null!;

    private Unit() : base(PrefabId.Create())
    {
    }

    public static Unit Create(PrefabId id, Name name)
    {   
        return new Unit
        {
            Id = id,
            Name = name
        };
    }
}