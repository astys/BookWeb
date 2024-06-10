using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Domain.DatabaseModels;

[PrimaryKey(nameof(Id))]
public class BookReview
{
    public required Guid Id { get; init; }
    [MaxLength(100)]
    public required string UserId { get; init; }
    public virtual ApplicationUser? User { get; set; }
    [MaxLength(100)]
    public required string BookId { get; init; }
    public virtual Book? Book { get; set; }
    public double Rating { get; set; }
    [MaxLength(2000)]
    public string? Comment { get; set; }
    public required DateTime CreatedAt { get; init; }
}