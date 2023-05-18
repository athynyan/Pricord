using Pricord.Domain.BattleRecords.Enums;

namespace Pricord.Domain.BattleRecords.ValueObjects;

public sealed record Damage
{
    public long Value { get; } 
    public BattleType BattleType { get; }

    private Damage(long value, BattleType battleType)
    {
        Value = value;
        BattleType = battleType;
    }

    private Damage ()
    {
    }

    public static Damage Create(long value, BattleType battleType)
    {
        return new Damage(value, battleType);
    }

}