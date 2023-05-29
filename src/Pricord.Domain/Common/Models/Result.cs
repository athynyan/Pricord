namespace Pricord.Domain.Common.Models;

public class Result<T>
{
    public T? Value { get; }
    public Error? Error { get; }
    public bool IsSuccess { get; } = false;

    private Result(T value)
    {
        Value = value;
        IsSuccess = true;
    }

    private Result(Error error)
    {
        Error = error;
    }

    public TReturn Match<TReturn>(Func<T, TReturn> onSuccess, Func<Error, TReturn> onFailure)
    {
        return IsSuccess ? onSuccess(Value!) : onFailure(Error!);
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(error);

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);
}