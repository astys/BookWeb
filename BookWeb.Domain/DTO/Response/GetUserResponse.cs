namespace BookWeb.Domain.DTO.Response;

public class GetUserResponse
{
    public required string EmailAddress { get; init; }
    public required string Name { get; init; }
    public required IEnumerable<string> FavouriteGenres { get; init; }
    public required int ReviewsCount { get; init; }
}