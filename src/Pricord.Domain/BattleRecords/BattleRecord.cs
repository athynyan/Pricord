using Pricord.Domain.BattleRecords.ValueObjects;
using Pricord.Domain.Common.Models;
using Pricord.Domain.Timelines;
using Pricord.Domain.Timelines.ValueObjects;
using Pricord.Domain.Units;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Domain.BattleRecords;

public sealed class BattleRecord : AggregateRoot<BattleRecordId, Guid>
{
	private HashSet<PlayableCharacter> _playableCharacters = new();

	private BattleRecord() : base(BattleRecordId.Create())
	{
	}

	public Damage ExpectedDamage { get; private set; } = default!;

	public BossId BossId { get; private set; } = default!;
	public Boss Boss { get; private set; } = default!;

	public TimelineId? TimelineId { get; private set; }
	public Timeline? Timeline { get; private set; } = default!;

	public ICollection<PlayableCharacter> PlayableCharacters => _playableCharacters!.ToList();

	public static BattleRecord Create(BossId bossId, PlayableCharacter[] playableCharacters, Damage expectedDamage)
	{
		if (playableCharacters.Length is > 5 or < 1)
			throw new InvalidOperationException("Playable character count for a battle record must be between 1 and 5");

		return new BattleRecord
		{
			BossId = bossId,
			ExpectedDamage = expectedDamage,
			_playableCharacters = new HashSet<PlayableCharacter>(playableCharacters)
		};
	}

	public static BattleRecord Create(BossId bossId, PlayableCharacter[] playableCharacters, Damage expectedDamage,
		TimelineId timelineId)
	{
		if (playableCharacters.Length is > 5 or < 1)
			throw new InvalidOperationException("Playable character count must be between 1 and 5");

		return new BattleRecord
		{
			BossId = bossId,
			ExpectedDamage = expectedDamage,
			_playableCharacters = new HashSet<PlayableCharacter>(playableCharacters),
			TimelineId = timelineId
		};
	}
}