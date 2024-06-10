using BookWeb.Application.Commands;
using BookWeb.Application.Repositories;
using BookWeb.Domain.DatabaseModels;
using BookWeb.Domain.ResultType;
using MediatR;

namespace BookWeb.Infrastructure.Handlers.Commands;

public class AddBookCommandHandler(IBookRepository bookRepository)
    : IRequestHandler<AddBookCommand, Result<EmptyResult>>
{
    public async Task<Result<EmptyResult>> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book
        {
            Id = Guid.NewGuid().ToString(),
            ImageUrl = request.Request.ImageUrl,
            GenreId = request.Request.GenreId,
            Description = request.Request.Description,
            Title = request.Request.Title,
            Author = request.Request.Author,
            CreatedAt = DateTime.Now
        };

        await bookRepository.AddAsync(book);
        await bookRepository.SaveChangesAsync();

        return Result<EmptyResult>.Success();
    }
}