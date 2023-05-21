using Pricord.Application.Timelines.Models;
using Pricord.Contracts.Timelines;
using Pricord.Domain.Timelines;
using Pricord.Domain.Timelines.Enums;
using Pricord.Domain.Timelines.ValueObjects;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Api.Timelines;

internal static class TimelineMapper
{
    public static CreateTimelineModel ToModel(this CreateTimelineDto result)
    {
        var items = result.Items.Select(p => p.ToModel()).ToArray();
        var video = result.Video?.ToModel();
        
        if (video is null)
        {
            return new CreateTimelineModel(items);
        }

        return new CreateTimelineModel(items, video);
    }

    public static TimelineDto ToDto(this Timeline timeline)
    {
        return new TimelineDto(
            timeline.Id.Value,
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

    public static TimelineItemModel ToModel(this TimelineItemDto dto)
    {
        return new TimelineItemModel(
            dto.Time,
            dto.AttackerId,
            dto.ActionType,
            dto.AdditionalInfo);
    }

    public static VideoModel ToModel(this VideoDto dto)
    {
        return new VideoModel(
            dto.Url,
            dto.Type);
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