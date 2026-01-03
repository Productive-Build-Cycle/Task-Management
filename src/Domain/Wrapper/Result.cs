#nullable enable

namespace TaskManagement.Application.Wrapper;

public class Result<T>
{
    #region Constructors

    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
    }

    private Result(string error)
    {
        IsSuccess = false;
        Error = error;
    }

    #endregion Constructors

    #region Properties

    public bool IsSuccess { get; }

    public T? Value { get; }

    public string Error { get; }

    #endregion Properties

    public static Result<T> Success(T value)
        => new(value);

    public static Result<T> Failure(string error)
        => new(error);
}

public class Result
{
    #region Constructor

    private Result(bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    #endregion Constructor

    public bool IsSuccess { get; }

    public string Error { get; }

    public static Result Success()
        => new(true, null);

    public static Result Failure(string error)
        => new(false, error);
}