using Pricord.Domain.Common.Models;

namespace Pricord.Domain.Units.ValueObjects;

public sealed record PlayableCharacterId : EntityId<Guid>
{
    private PlayableCharacterId(Guid value) : base(value)
    {
    }

    private PlayableCharacterId() : base(Guid.NewGuid())
    {
    }

    public static PlayableCharacterId Create(Guid value)
    {
        return new(value);
    }

    public static PlayableCharacterId Create()
    {
        return new();
    }
}