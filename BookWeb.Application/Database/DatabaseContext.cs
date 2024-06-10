using BookWeb.Domain.DatabaseModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Application.Database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Genre> Genres { get; set; }
    public DbSet<ApplicationUserGenre> UserGenres { get; set; }
    public DbSet<BookReview> BookReviews { get; set; }
    public DbSet<Book> Books { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<ApplicationUserGenre>()
            .HasOne(x => x.User)
            .WithMany(x => x.UserGenres)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ApplicationUserGenre>()
            .HasOne(x => x.Genre)
            .WithMany(x => x.UserGenres)
            .HasForeignKey(x => x.GenreId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Book>()
            .HasOne(x => x.Genre)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.GenreId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<BookReview>()
            .HasOne(x => x.User)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<BookReview>()
            .HasOne(x => x.Book)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}