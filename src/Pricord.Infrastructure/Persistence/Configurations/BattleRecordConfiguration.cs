using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pricord.Domain.BattleRecords;
using Pricord.Domain.BattleRecords.ValueObjects;

namespace Pricord.Infrastructure.Persistence.Configurations;

internal sealed class BattleRecordConfiguration : IEntityTypeConfiguration<BattleRecord>
{
	public void Configure(EntityTypeBuilder<BattleRecord> builder)
	{
		builder.HasKey(battleRecord => battleRecord.Id);

		builder.Property(battleRecord => battleRecord.Id)
			.HasConversion(
				battleRecordId => battleRecordId.Value,
				value => BattleRecordId.Create(value));

		builder.OwnsOne(battleRecord => battleRecord.ExpectedDamage, damageBuilder =>
		{
			damageBuilder.Property(y => y.Value)
				.HasColumnName("ExpectedDamage");
			damageBuilder.Property(y => y.BattleType)
				.HasColumnName("BattleType");
		});

		builder.HasOne(b => b.Boss)
			.WithMany()
			.HasForeignKey(b => b.BossId);

		builder.HasOne(b => b.Timeline)
			.WithOne()
			.HasForeignKey<BattleRecord>(b => b.TimelineId);
	}
}