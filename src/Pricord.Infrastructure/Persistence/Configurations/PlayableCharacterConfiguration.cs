using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pricord.Domain.Units;
using Pricord.Domain.Units.ValueObjects;

namespace Pricord.Infrastructure.Persistence.Configurations;

internal sealed class PlayableCharacterConfiguration : IEntityTypeConfiguration<PlayableCharacter>
{
    public void Configure(EntityTypeBuilder<PlayableCharacter> builder)
    {
        builder.HasKey(PlayableCharacter => PlayableCharacter.Id);

        builder.Property(PlayableCharacter => PlayableCharacter.Id)
            .HasConversion(
                playableCharacterId => playableCharacterId.Value,
                value => PlayableCharacterId.Create(value));
        
        builder.Property(PlayableCharacter => PlayableCharacter.Level)
            .HasConversion(
                level => level.Value,
                value => Level.Create(value));
        
        builder.Property(PlayableCharacter => PlayableCharacter.Rank)
            .HasConversion(
                rank => rank.Value,
                value => Rank.Create(value));
        
        builder.Property(PlayableCharacter => PlayableCharacter.Rarity)
            .HasConversion(
                rarity => rarity.Value,
                value => Rarity.Create(value));

        builder.HasOne(PlayableCharacter => PlayableCharacter.Unit)
            .WithMany()
            .HasForeignKey(PlayableCharacter => PlayableCharacter.PrefabId);
    }
}
