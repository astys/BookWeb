using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BookWeb.Domain.DatabaseModels;

public class ApplicationUser : IdentityUser
{
    [MaxLength(50)]
    public string? Name { get; init; }
    public virtual ICollection<ApplicationUserGenre>? UserGenres { get; set; }
    public virtual ICollection<BookReview>? Reviews { get; set; }
}