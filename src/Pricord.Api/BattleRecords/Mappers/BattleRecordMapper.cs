using Pricord.Api.BattleRecords.Contracts;
using Pricord.Api.Timelines.Mappers;
using Pricord.Application.BattleRecords.Commands.CreateBattleRecord;
using Pricord.Application.BattleRecords.Contracts;

namespace Pricord.Api.BattleRecords.Mappers;

internal static class BattleRecordMapper
{
    public static CreateBattleRecordCommand ToCommand(this CreateBattleRecordRequest request)
    {
        return new CreateBattleRecordCommand(
            request.Boss,
            request.PlayableCharacters.ToArray(),
            request.ExpectedDamage,
            request.BattleType,
            request.Timeline);
    }

    public static CreateBattleRecordResponse ToResponse(this BattleRecordResult record)
    {
        return new CreateBattleRecordResponse(
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