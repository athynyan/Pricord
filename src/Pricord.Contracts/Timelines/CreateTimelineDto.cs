namespace Pricord.Contracts.Timelines;

public sealed record CreateTimelineDto(
    IEnumerable<TimelineItemDto> Items,
    VideoDto? Video);