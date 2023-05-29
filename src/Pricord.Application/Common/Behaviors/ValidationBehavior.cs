using System.Reflection;
using FluentValidation;
using MediatR;

namespace Pricord.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var resultType = typeof(TResponse);
            var failMethod = resultType.GetMethod("Failure", BindingFlags.Public | BindingFlags.Static);
            var result = (TResponse)failMethod?.Invoke(null, new object[] { new ValidationException(validationResult.Errors) })!;
            return result;
        }

        return await next();
    }
}
