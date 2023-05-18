using Pricord.Domain.Common.Models;

namespace Pricord.Domain.Authentication.ValueObjects;

public sealed record TokenId : EntityId<Guid>
{
    private TokenId(Guid value) : base(value)
    {
    }

    private TokenId() : base(Guid.Empty)
    {
    }

    public static TokenId Create()
    {
        return new(Guid.NewGuid());
    }

    public static TokenId Create(Guid value)
    {
        return new(value);
    }
}