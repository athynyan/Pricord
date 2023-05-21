using Pricord.Api.Timelines;
using Pricord.Application.BattleRecords.Commands.CreateBattleRecord;
using Pricord.Application.BattleRecords.Models;
using Pricord.Contracts.BattleRecords;

namespace Pricord.Api.BattleRecords.Mappers;

internal static class BattleRecordMapper
{
    public static CreateBattleRecordCommand ToCommand(this CreateBattleRecordRequest request)
    {
        return new CreateBattleRecordCommand(
            request.Boss.ToEntity(),
            request.PlayableCharacters.Select(pc => pc.ToEntity()).ToArray(),
            request.ExpectedDamage,
            request.BattleType,
            request.Timeline?.ToModel());
    }

    public static BattleRecordResponse ToResponse(this BattleRecordResult record)
    {
        return new BattleRecordResponse(
            record.Id.Value,
            record.ExpectedDamage.Value,
            record.ExpectedDamage.BattleType.ToString(),
            record.BossId.Value,
            record.PlayableCharacterIds
                .Select(p => p.Value)
                .ToArray(),
            record.TimelineId?.Value);
    }

    public static BattleRecordDetailsResponse ToResponse(this BattleRecordDetailsResult record)
    {
        return new BattleRecordDetailsResponse(
            record.Id.Value,
            record.Boss.ToDto(),
            record.ExpectedDamage.Value,
            record.ExpectedDamage.BattleType.ToString(),
            record.PlayableCharacters
                .Select(pc => pc.ToDto())
                .ToArray(),
            record.Timeline?.ToDto());
    }
}