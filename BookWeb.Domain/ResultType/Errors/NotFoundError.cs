namespace BookWeb.Domain.ResultType.Errors;

public class NotFoundError : ResultError
{
    private NotFoundError(string type, string description)
        : base(type, description) { }

    public static ResultError New(string entity)
        => new NotFoundError(
            nameof(NotFoundError), 
            $"{entity} not found."
        );
}