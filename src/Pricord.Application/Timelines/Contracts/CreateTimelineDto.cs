using Pricord.Application.Timelines.Contracts.Dtos;

namespace Pricord.Application.Timelines.Contracts;

public sealed record CreateTimelineDto(
    IEnumerable<TimelineItemDto> Items,
    VideoDto? Video);