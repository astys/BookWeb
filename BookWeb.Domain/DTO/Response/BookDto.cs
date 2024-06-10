namespace BookWeb.Domain.DTO.Response;

public class BookDto
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required string Author { get; init; }
    public required string? Description { get; init; }
    public required string? ImageUrl { get; init; }
    public required string Genre { get; init; }
    public required double Rating { get; init; }
    public bool AlreadyReviewed { get; init; }
    public IEnumerable<ReviewDto>? Reviews { get; init; }
}