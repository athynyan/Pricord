using Pricord.Domain.BattleRecords.ValueObjects;
using Pricord.Domain.Timelines;
using Pricord.Domain.Units;

namespace Pricord.Application.BattleRecords.Models;

public sealed record BattleRecordDetailsResult(
    BattleRecordId Id,
    Boss Boss,
    Damage ExpectedDamage,
    Timeline? Timeline,
    PlayableCharacter[] PlayableCharacters);