using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Domain.DatabaseModels;

[PrimaryKey(nameof(Id))]
public class Genre
{
    [MaxLength(100)]
    public required string Id { get; init; }
    
    [MaxLength(100)]
    public required string Name { get; init; }
    public virtual ICollection<ApplicationUserGenre>? UserGenres { get; set; }
    public virtual ICollection<Book>? Books { get; set; }
}