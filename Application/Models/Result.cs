namespace Application.Models;

public class Result<T>
{
    public bool Success { get; init; }
    public string ErrorMessage { get; init; } = string.Empty;
    public T? Value { get; set; }

    private Result(T value)
    {
        Success = true;
        Value = value;
    }

    private Result(string error)
    {
        Success = false;
        ErrorMessage = error;
    }

    public static Result<T> Ok(T value) => new(value);

    public static Result<T> Fail(string error) => new(error);
}