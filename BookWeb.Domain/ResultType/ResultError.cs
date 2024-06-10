namespace BookWeb.Domain.ResultType;

public abstract class ResultError(string type, string description)
{
    public string Type { get; set; } = type;
    public string Description { get; set; } = description;
}