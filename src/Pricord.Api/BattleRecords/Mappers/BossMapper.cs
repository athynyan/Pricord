using Pricord.Application.BattleRecords.Contracts.Dtos;
using Pricord.Domain.Units;
using Pricord.Domain.Units.Enums;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Api.BattleRecords.Mappers;

internal static class BossMapper
{
    public static Boss ToEntity(this BossDto dto)
    {
        return Boss.Create(
            PrefabId.Create(dto.PrefabId),
            Level.Create(dto.Level),
            Health.Create(dto.Health),
            (Tier)dto.Tier);
    }

    public static BossDto ToDto(this Boss boss)
    {
        return new BossDto(
            boss.PrefabId.Value,
            boss.Level.Value,
            boss.Health.Value,
            (int)boss.Tier);
    }
}