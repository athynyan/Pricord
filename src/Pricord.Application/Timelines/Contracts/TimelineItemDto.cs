namespace Pricord.Application.Timelines.Contracts.Dtos;

public sealed record TimelineItemDto(
    int Time,
    string AttackerId,
    string ActionType,
    string? AdditionalInfo);