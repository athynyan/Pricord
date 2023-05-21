namespace Pricord.Contracts.Common.Abstractions;

public interface IRequest<out TResponse>
    where TResponse : IResponse
{
}