using Pricord.Domain.Common.Models;

namespace Pricord.Domain.Authentication.ValueObjects;

public sealed record UserId : EntityId<Guid>
{
    private UserId(Guid value) : base(value)
    {
    }

    private UserId() : base(Guid.Empty)
    {
    }

    public static UserId Create()
    {
        return new(Guid.NewGuid());
    }

    public static UserId Create(Guid value)
    {
        return new(value);
    }
}
