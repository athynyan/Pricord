using Pricord.Application.BattleRecords.Contracts.Dtos;
using Pricord.Domain.Timelines;
using Pricord.Domain.Timelines.Enums;
using Pricord.Domain.Timelines.ValueObjects;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Api.BattleRecords.Mappers;

internal static class TimelineMapper
{
    public static Timeline ToEntity(this TimelineDto dto)
    {   
        var items = dto.Items.Select(p => p.ToEntity()).ToArray();
        var video = dto.Video?.ToValueObject();
        
        if (video is null)
        {
            return Timeline.Create(items);
        }

        return Timeline.Create(items, video);
    }

    public static TimelineDto ToDto(this Timeline timeline)
    {
        return new TimelineDto(
            timeline.Items.Select(p => p.ToDto()).ToArray(),
            timeline.Video?.ToDto());
    }

    public static TimelineItemDto ToDto(this TimelineItem item)
    {
        return new TimelineItemDto(
            item.Time,
            item.AttackerId.Value,
            item.ActionType.ToString(),
            item.AdditionalInfo);
    }

    public static TimelineItem ToEntity(this TimelineItemDto dto)
    {
        return TimelineItem.Create(
            dto.Time,
            PrefabId.Create(dto.AttackerId),
            Enum.Parse<ActionType>(dto.ActionType, true),
            dto.AdditionalInfo);
    }

    private static Video ToValueObject(this VideoDto dto)
    {
        return Video.Create(
            dto.Url, 
            Enum.Parse<VideoType>(dto.Type, true));
    }

    private static VideoDto ToDto(this Video video)
    {
        return new VideoDto(
            video.Url,
            video.Type.ToString());
    }
}