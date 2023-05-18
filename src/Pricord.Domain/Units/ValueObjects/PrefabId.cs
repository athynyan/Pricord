using Pricord.Domain.Common.Models;

namespace Pricord.Domain.Units.ValueObjects;

public sealed record PrefabId : EntityId<string>
{
    private PrefabId(string value) : base(value)
    {
    }

    private PrefabId() : base(string.Empty)
    {
    }

    public static PrefabId Create(string value)
    {
        return new(value);
    }

    public static PrefabId Create()
    {
        return new();
    }
}