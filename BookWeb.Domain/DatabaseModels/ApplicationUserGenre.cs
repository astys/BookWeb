using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Domain.DatabaseModels;

[PrimaryKey(nameof(Id))]
public class ApplicationUserGenre
{
    public required Guid Id { get; init; }
    [MaxLength(100)]
    public required string UserId { get; init; }
    public virtual ApplicationUser? User { get; set; }
    [MaxLength(100)]
    public required string GenreId { get; init; }
    public virtual Genre? Genre { get; set; }
}