namespace Pricord.Application.Timelines.Models;

public sealed record CreateTimelineModel(
    IEnumerable<TimelineItemModel> Items,
    VideoModel? Video = null);