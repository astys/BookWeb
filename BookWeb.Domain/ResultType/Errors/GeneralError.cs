namespace BookWeb.Domain.ResultType.Errors;

public class GeneralError : ResultError
{
    private GeneralError(string type, string description)
        : base(type, description) { }

    public static ResultError New(string message)
        => new GeneralError(
            nameof(GeneralError), 
            message
        );
}