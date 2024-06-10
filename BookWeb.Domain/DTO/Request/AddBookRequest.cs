namespace BookWeb.Domain.DTO.Request;

public class AddBookRequest
{
    public required string Title { get; init; }
    public string? ImageUrl { get; init; }
    public required string Author { get; init; }
    public string? Description { get; init; }
    public required string GenreId { get; init; }
}