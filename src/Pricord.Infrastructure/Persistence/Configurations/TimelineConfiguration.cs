using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pricord.Domain.Timelines;
using Pricord.Domain.Timelines.ValueObjects;

namespace Pricord.Infrastructure.Persistence.Configurations;

internal sealed class TimelineConfiguration : IEntityTypeConfiguration<Timeline>
{
	public void Configure(EntityTypeBuilder<Timeline> builder)
	{
		builder.HasKey(timeline => timeline.Id);

		builder.Property(timeline => timeline.Id)
			.HasConversion(
				timelineId => timelineId.Value,
				value => TimelineId.Create(value));

		builder.HasMany(t => t.Items)
			.WithOne(e => e.Timeline!)
			.HasForeignKey(e => e.TimelineId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.OwnsOne(timeline => timeline.Video, videoBuilder =>
		{
			videoBuilder.Property(y => y.Url)
				.HasColumnName("VideoUrl");
			videoBuilder.Property(y => y.Type)
				.HasColumnName("VideoType");
		});
	}
}