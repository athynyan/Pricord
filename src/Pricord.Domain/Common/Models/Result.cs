namespace Pricord.Domain.Common.Models;

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(T value) : base()
    {
        Value = value;
    }

    private Result(Exception error) : base(error)
    {
        Value = default;
    }

    public static Result<T> Success(T value) => new(value);
    public static new Result<T> Failure(Exception error) => new(error);
    public TReturn Match<TReturn>(Func<T, TReturn> onSuccess, Func<Exception, TReturn> onFailure)
    {
        return IsSuccess ? onSuccess(Value!) : onFailure(Error!);
    }

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Exception error) => Failure(error);
}

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Exception? Error { get; }

    protected Result(Exception error)
    {
        IsSuccess = false;
        Error = error;
    }

    protected Result()
    {
        IsSuccess = true;
        Error = default;
    }

    public static Result Success() => new();
    public static Result Failure(Exception error) => new(error);

    public static implicit operator Result(Exception error) => Result<object>.Failure(error);
}