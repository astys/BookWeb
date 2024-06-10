namespace BookWeb.Domain.DTO.Response;

public class ReviewDto
{
    public required string UserId { get; init; }
    public required string Name { get; init; }
    public required DateTime Date { get; init; }
    public required string? Comment { get; init; }
    public required double? Rating { get; init; }
}