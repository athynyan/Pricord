using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pricord.Domain.Common.ValueObjects;
using Pricord.Domain.Units;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Infrastructure.Persistence.Configurations;

internal sealed class UnitConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                value => PrefabId.Create(value));
        
        builder.Property(u => u.Name)
            .HasConversion(
                name => name.Value,
                value => Name.Create(value));
    }
}
