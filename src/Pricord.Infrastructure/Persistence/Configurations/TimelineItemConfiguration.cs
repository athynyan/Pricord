using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pricord.Domain.Timelines;
using Pricord.Domain.Timelines.ValueObjects;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Infrastructure.Persistence.Configurations;

internal sealed class TimelineItemConfiguration : IEntityTypeConfiguration<TimelineItem>
{
    public void Configure(EntityTypeBuilder<TimelineItem> builder)
    {
        builder.HasKey(timelineItem => timelineItem.Id);

        builder.Property(timelineItem => timelineItem.Id)
            .HasConversion(
                timelineItemId => timelineItemId.Value,
                value => TimelineItemId.Create(value));
        
        builder.Property(timelineItem => timelineItem.AttackerId)
            .HasConversion(
                attackerId => attackerId.Value,
                value => PrefabId.Create(value));
    }
}
