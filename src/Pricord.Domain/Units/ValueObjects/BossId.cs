using Pricord.Domain.Common.Models;

namespace Pricord.Domain.Units.ValueObjects;

public sealed record BossId : EntityId<Guid>
{

    private BossId(Guid value) : base(value)
    {
    }

    private BossId() : base(Guid.NewGuid())
    {
    }

    public static BossId Create(Guid value)
    {
        return new(value);
    }

    public static BossId Create()
    {
        return new();
    }
}