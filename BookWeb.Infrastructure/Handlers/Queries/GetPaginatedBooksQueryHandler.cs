using BookWeb.Application.Queries;
using BookWeb.Application.Repositories;
using BookWeb.Domain.DatabaseModels;
using BookWeb.Domain.DTO.Response;
using BookWeb.Domain.ResultType;
using BookWeb.Domain.ResultType.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Infrastructure.Handlers.Queries;

public class GetPaginatedBooksQueryHandler(IBookRepository bookRepository) 
    : IRequestHandler<GetPaginatedBooksQuery, Result<GetBooksResponse>>
{
    public async Task<Result<GetBooksResponse>> Handle(GetPaginatedBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await bookRepository
            .GetAll()
            .Where(x => x.Title.ToLower().Contains(request.Title.ToLower()))
            .OrderBy(x => x.Title)
            .Skip((request.PaginationParameters.PageNumber - 1) * request.PaginationParameters.PageSize)
            .Take(request.PaginationParameters.PageSize)
            .ToListAsync(cancellationToken);

        var totalBooks = await bookRepository.GetAll()
            .Where(x => x.Title.ToLower().Contains(request.Title.ToLower()))
            .CountAsync(cancellationToken);

        if (books.Count == 0)
        {
            return Result<GetBooksResponse>.Failure(NotFoundError.New(nameof(Book)));
        }
        
        var response = new GetBooksResponse()
        {
            Books = books.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Genre = x.Genre?.Name ?? string.Empty,
                Author = x.Author,
                Rating = GetRating(x.Reviews?.ToList())
            }),
            TotalBooks = totalBooks
        };

        return Result<GetBooksResponse>.Success(response);
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