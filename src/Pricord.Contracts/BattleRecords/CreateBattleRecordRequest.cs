using Pricord.Contracts.Common.Abstractions;
using Pricord.Contracts.Timelines;

namespace Pricord.Contracts.BattleRecords;

public sealed record CreateBattleRecordRequest(
    BossDto Boss,
    PlayableCharacterDto[] PlayableCharacters,
    int ExpectedDamage,
    string BattleType,
    CreateTimelineDto? Timeline) : IRequest<BattleRecordResponse>;