using Pricord.Domain.Timelines.ValueObjects;

namespace Pricord.Application.Timelines.Contracts.Dtos;

public sealed record TimelineDto(
    Guid Id,
    IEnumerable<TimelineItemDto> Items,
    VideoDto? Video);