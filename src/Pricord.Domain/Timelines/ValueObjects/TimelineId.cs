using Pricord.Domain.Common.Models;

namespace Pricord.Domain.Timelines.ValueObjects;

public sealed record TimelineId : EntityId<Guid>
{
    private TimelineId(Guid value) : base(value)
    {
    }

    private TimelineId() : base(Guid.NewGuid())
    {
    }

    public static TimelineId Create()
    {
        return new();
    }

    public static TimelineId Create(Guid value)
    {
        return new(value);
    }
}
