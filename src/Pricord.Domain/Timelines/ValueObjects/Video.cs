using Pricord.Domain.Timelines.Enums;

namespace Pricord.Domain.Timelines.ValueObjects;

public sealed record Video
{
    public string Url { get; } = default!;
    public VideoType Type { get; }

    private Video(string url, VideoType type)
    {
        Url = url;
        Type = type;
    }

    public static Video Create(string url, VideoType type)
    {
        return new(url, type);
    }
}