using Pricord.Domain.Common.Models;

namespace Pricord.Domain.Timelines.ValueObjects;

public sealed record TimelineItemId : EntityId<Guid>
{
    private TimelineItemId(Guid value) : base(value)
    {
    }

    private TimelineItemId() : base(Guid.NewGuid())
    {
    }

    public static TimelineItemId Create()
    {
        return new();
    }

    public static TimelineItemId Create(Guid value)
    {
        return new(value);
    }
}