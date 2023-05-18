namespace Pricord.Application.BattleRecords.Contracts.Dtos;

public sealed record TimelineDto(
    IEnumerable<TimelineItemDto> Items,
    VideoDto? Video);