namespace BookWeb.Domain.DTO.Request;

public class AddReviewRequest
{
    public required string BookId { get; init; }
    public double Rating { get; init; }
    public string? Comment { get; init; }
}