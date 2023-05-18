using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pricord.Domain.Authentication;
using Pricord.Domain.Authentication.ValueObjects;

namespace Pricord.Infrastructure.Persistence.Configurations;

internal sealed class TokenConfiguration : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.HasKey(token => token.Id);

		builder.Property(token => token.Id)
			.HasConversion(
				tokenId => tokenId.Value,
				value => TokenId.Create(value));
        
        builder.HasOne(token => token.User)
            .WithMany(user => user.Tokens)
            .HasForeignKey(token => token.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
