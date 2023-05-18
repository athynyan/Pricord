using Pricord.Domain.Common.Models;
using Pricord.Domain.Timelines.Enums;
using Pricord.Domain.Timelines.ValueObjects;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Domain.Timelines;

public sealed class TimelineItem : Entity<TimelineItemId, Guid>
{
    public int Time { get; private set; }
	public PrefabId AttackerId { get; private set; } = default!;
	public ActionType ActionType { get; private set; }
	public string? AdditionalInfo { get; private set; }

	public TimelineId TimelineId { get; private set; } = default!;
	public Timeline? Timeline { get; } = default!;

    private TimelineItem() : base(TimelineItemId.Create())
    {
    }
	
	public static TimelineItem Create(int time, PrefabId attackerId, ActionType actionType, string? additionalInfo = null)
	{
		return new TimelineItem
		{
			Time = time,
			AttackerId = attackerId,
			ActionType = actionType,
			AdditionalInfo = additionalInfo
		};
	}
}
