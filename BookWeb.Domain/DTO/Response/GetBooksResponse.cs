namespace BookWeb.Domain.DTO.Response;

public class GetBooksResponse
{
    public required IEnumerable<BookDto> Books { get; init; }
    public required int TotalBooks { get; init; }
}