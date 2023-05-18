namespace Pricord.Application.BattleRecords.Contracts.Dtos;

public sealed record TimelineItemDto(
    int Time,
    string AttackerId,
    string ActionType,
    string? AdditionalInfo);