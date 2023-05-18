using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pricord.Domain.Units;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Infrastructure.Persistence.Configurations;

internal sealed class BossConfiguration : IEntityTypeConfiguration<Boss>
{
    public void Configure(EntityTypeBuilder<Boss> builder)
    {
        builder.HasKey(boss => boss.Id);

        builder.Property(boss => boss.Id)
            .HasConversion(
                bossId => bossId.Value,
                value => BossId.Create(value));

        builder.Property(boss => boss.Level)
            .HasConversion(
                level => level.Value,
                value => Level.Create(value));
        
        builder.Property(boss => boss.Health)
            .HasConversion(
                health => health.Value,
                value => Health.Create(value));

        builder.HasOne(boss => boss.Unit)
            .WithMany()
            .HasForeignKey(boss => boss.PrefabId);
    }
}
