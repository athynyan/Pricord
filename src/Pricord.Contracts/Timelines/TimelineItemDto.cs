namespace Pricord.Contracts.Timelines;

public sealed record TimelineItemDto(
    int Time,
    string AttackerId,
    string ActionType,
    string? AdditionalInfo);