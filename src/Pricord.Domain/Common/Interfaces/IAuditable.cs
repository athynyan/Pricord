namespace Pricord.Domain.Common.Interfaces;

public interface IAuditable
{
    DateTime CreatedAt { get; set; }
    DateTime? LastModified { get; set; }
}