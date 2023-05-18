using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pricord.Domain.Authentication;
using Pricord.Domain.Authentication.ValueObjects;
using Pricord.Domain.Common.ValueObjects;

namespace Pricord.Infrastructure.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

		builder.Property(user => user.Id)
			.HasConversion(
				userId => userId.Value,
				value => UserId.Create(value));

		builder.Property(user => user.Name)
			.HasConversion(
				name => name.Value,
				value => Name.Create(value));

		builder.Property(user => user.Email)
			.HasConversion(
				email => email!.Value,
				value => Email.Create(value));

		builder.Property(user => user.Password)
			.HasConversion(
				password => password.Hash,
				value => Password.Create(value));
		
		builder.HasMany(user => user.Tokens)
			.WithOne(token => token.User)
			.HasForeignKey(token => token.UserId)
			.OnDelete(DeleteBehavior.Cascade);
    }
}
