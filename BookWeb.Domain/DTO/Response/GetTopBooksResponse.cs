namespace BookWeb.Domain.DTO.Response;

public class GetTopBooksResponse
{
    public required IEnumerable<BookDto> HighestRated { get; init; }
    public required IEnumerable<BookDto> RecentlyAdded { get; init; }
}