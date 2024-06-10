namespace BookWeb.Domain.ResultType;

public class Result(ResultError? error)
{
    public ResultError? Error { get; protected init; } = error;
    public static Result Failure(ResultError error) => new(error);
}

public class Result<T>: Result where T : class
{
    public bool IsSuccess => Error is null;
    public bool IsFailure => Error is not null;
    
    public T? Value { get; init; }
    
    private Result(ResultError? error, T? value) : base(error)
    {
        Error = error;
        Value = value;
    }

    public static Result<T> Success(T? value = null) => new(null, value);
    public new static Result<T> Failure(ResultError error) => new(error, null);
}