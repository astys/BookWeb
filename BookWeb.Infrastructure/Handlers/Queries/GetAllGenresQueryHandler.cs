using BookWeb.Application.Queries;
using BookWeb.Application.Repositories;
using BookWeb.Domain.DatabaseModels;
using BookWeb.Domain.DTO.Response;
using BookWeb.Domain.ResultType;
using BookWeb.Domain.ResultType.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Infrastructure.Handlers.Queries;

public class GetAllGenresQueryHandler(IGenreRepository genreRepository) 
    : IRequestHandler<GetAllGenresQuery, Result<GetAllGenresResponse>>
{
    public async Task<Result<GetAllGenresResponse>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        var genres = await genreRepository.GetAll().ToListAsync(cancellationToken);

        if (genres.Count == 0)
        {
            return Result<GetAllGenresResponse>.Failure(NotFoundError.New(nameof(Genre)));
        }
        
        var response = new GetAllGenresResponse
        {
            Genres = genres.Select(x => new GenreDto
            {
                Id = x.Id,
                Name = x.Name
            })
        };

        return Result<GetAllGenresResponse>.Success(response);
    }
}