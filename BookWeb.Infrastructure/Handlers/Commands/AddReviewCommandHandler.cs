using System.Security.Claims;
using BookWeb.Application.Commands;
using BookWeb.Application.Repositories;
using BookWeb.Domain.DatabaseModels;
using BookWeb.Domain.ResultType;
using MediatR;

namespace BookWeb.Infrastructure.Handlers.Commands;

public class AddReviewCommandHandler(IBookReviewRepository bookReviewRepository)
    : IRequestHandler<AddReviewCommand, Result<EmptyResult>>
{
    public async Task<Result<EmptyResult>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = request.ClaimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var review = new BookReview
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            BookId = request.Request.BookId,
            Comment = request.Request.Comment,
            Rating = request.Request.Rating,
            CreatedAt = DateTime.Now
        };

        await bookReviewRepository.AddAsync(review);
        await bookReviewRepository.SaveChangesAsync();

        return Result<EmptyResult>.Success();
    }
}