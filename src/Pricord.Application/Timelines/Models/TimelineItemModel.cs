namespace Pricord.Application.Timelines.Models;

public sealed record TimelineItemModel(
    int Time,
    string AttackerId,
    string ActionType,
    string? AdditionalInfo);