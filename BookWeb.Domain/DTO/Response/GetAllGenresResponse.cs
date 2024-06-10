namespace BookWeb.Domain.DTO.Response;

public class GetAllGenresResponse
{
    public required IEnumerable<GenreDto> Genres { get; init; }
}