using System.Security.Claims;
using BookWeb.Application.Queries;
using BookWeb.Application.Repositories;
using BookWeb.Domain.DatabaseModels;
using BookWeb.Domain.DTO.Response;
using BookWeb.Domain.ResultType;
using BookWeb.Domain.ResultType.Errors;
using Duende.IdentityServer.Extensions;
using MediatR;

namespace BookWeb.Infrastructure.Handlers.Queries;

public class GetBookByIdQueryHandler(IBookRepository bookRepository)
    : IRequestHandler<GetBookByIdQuery, Result<BookDto>>
{
    public async Task<Result<BookDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetByIdAsync(request.BookId);
        
        if (book is null)
        {
            return Result<BookDto>.Failure(NotFoundError.New(nameof(Book)));
        }

        var mappedBook = new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            ImageUrl = book.ImageUrl,
            Genre = book.Genre?.Name ?? string.Empty,
            Author = book.Author,
            Rating = GetRating(book.Reviews?.ToList()),
            Reviews = GetReviews(book.Reviews?.ToList()),
            AlreadyReviewed = AlreadyReviewed(book.Reviews?.ToList(), request.ClaimsPrincipal)
        };

        return Result<BookDto>.Success(mappedBook);
    }
    
    private static double GetRating(IList<BookReview>? reviews)
    {
        if (reviews is null || reviews.Count == 0)
        {
            return 0;
        }
        var averageReview = reviews.Select(x => x.Rating).Average();
        return averageReview;
    }

    private static IEnumerable<ReviewDto> GetReviews(IList<BookReview>? reviews)
    {
        if (reviews is null || reviews.Count == 0)
        {
            return [];
        }

        return reviews
            .OrderBy(x => x.CreatedAt)
            .Select(x => new ReviewDto
            {
                UserId = x.UserId,
                Date = x.CreatedAt,
                Name = x.User?.Name ?? string.Empty,
                Comment = x.Comment,
                Rating = x.Rating
            });
    }
    
    private static bool AlreadyReviewed(IList<BookReview>? reviews, ClaimsPrincipal? claimsPrincipal)
    {
        if (reviews is null || reviews.Count == 0 || claimsPrincipal?.IsAuthenticated() == false)
        {
            return false;
        }
        
        var userId = claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return false;
        }

        return reviews
            .Select(x => x.UserId)
            .Contains(userId);
    }
}