namespace Pricord.Contracts.Timelines;

public sealed record TimelineDto(
    Guid Id,
    IEnumerable<TimelineItemDto> Items,
    VideoDto? Video);