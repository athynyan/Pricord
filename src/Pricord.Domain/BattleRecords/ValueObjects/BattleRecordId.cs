using Pricord.Domain.Common.Models;

namespace Pricord.Domain.BattleRecords.ValueObjects;

public sealed record BattleRecordId : EntityId<Guid>
{
    private BattleRecordId(Guid value) : base(value)
    {
    }

    private BattleRecordId() : base(Guid.NewGuid())
    {
    }

    public static BattleRecordId Create()
    {
        return new();
    }

    public static BattleRecordId Create(Guid value)
    {
        return new(value);
    }
}