using Pricord.Domain.Common.Models;
using Pricord.Domain.Timelines.ValueObjects;

namespace Pricord.Domain.Timelines;

public sealed class Timeline : AggregateRoot<TimelineId, Guid>
{
    private HashSet<TimelineItem> _items = new();
    public Video? Video { get; private set; }

    public IReadOnlyCollection<TimelineItem> Items => _items;

    private Timeline() : base(TimelineId.Create())
    {
    }

    public static Timeline Create(IEnumerable<TimelineItem> items)
    {
        return new Timeline
        {
            _items = new HashSet<TimelineItem>(items)
        };
    }

    public static Timeline Create(IEnumerable<TimelineItem> items, Video video)
    {
        return new Timeline
        {
            _items = new HashSet<TimelineItem>(items),
            Video = video
        };
    }
}
