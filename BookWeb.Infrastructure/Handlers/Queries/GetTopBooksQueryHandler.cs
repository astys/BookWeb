using BookWeb.Application.Queries;
using BookWeb.Application.Repositories;
using BookWeb.Domain.DatabaseModels;
using BookWeb.Domain.DTO.Response;
using BookWeb.Domain.ResultType;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Infrastructure.Handlers.Queries;

public class GetTopBooksQueryHandler(IBookRepository bookRepository)
    : IRequestHandler<GetTopBooksQuery, Result<GetTopBooksResponse>>
{
    public async Task<Result<GetTopBooksResponse>> Handle(GetTopBooksQuery request, CancellationToken cancellationToken)
    {
        var highestRated = await bookRepository
            .GetAll()
            .Where(x => x.Reviews != null && x.Reviews.Count > 0)
            .OrderByDescending(x => x.Reviews!.Select(r => r.Rating).Average())
            .ThenBy(x => x.Title)
            .Take(5)
            .ToListAsync(cancellationToken);

        var recentlyAdded = await bookRepository.GetAll()
            .OrderByDescending(x => x.CreatedAt)
            .ThenBy(x => x.Title)
            .Take(5)
            .ToListAsync(cancellationToken);
        
        var response = new GetTopBooksResponse
        {
            HighestRated = highestRated.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Genre = x.Genre?.Name ?? string.Empty,
                Author = x.Author,
                Rating = GetRating(x.Reviews?.ToList())
            }),
            RecentlyAdded = recentlyAdded.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Genre = x.Genre?.Name ?? string.Empty,
                Author = x.Author,
                Rating = GetRating(x.Reviews?.ToList())
            }),
        };

        return Result<GetTopBooksResponse>.Success(response);
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
}