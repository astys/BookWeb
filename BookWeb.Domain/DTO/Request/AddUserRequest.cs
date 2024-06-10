using System.Text.Json.Serialization;

namespace BookWeb.Domain.DTO.Request;

public class AddUserRequest
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required IEnumerable<string> Genres { get; init; }
    public required string Name { get; init; }
}