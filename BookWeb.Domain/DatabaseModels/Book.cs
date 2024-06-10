using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Domain.DatabaseModels;

[PrimaryKey(nameof(Id))]
public class Book
{
    [MaxLength(100)]
    public required string Id { get; init; }
    [MaxLength(100)]
    public required string Title { get; init; }
    [MaxLength(100)]
    public required string Author { get; init; }
    [MaxLength(2000)]
    public string? Description { get; init; }
    [MaxLength(2000)]
    public string? ImageUrl { get; init; }
    [MaxLength(100)]
    public required string GenreId { get; init; }
    public required DateTime CreatedAt { get; init; }
    public virtual Genre? Genre { get; set; }
    public virtual ICollection<BookReview>? Reviews { get; set; }
}