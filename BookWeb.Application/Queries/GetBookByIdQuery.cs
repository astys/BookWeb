using System.Security.Claims;
using BookWeb.Domain.DTO.Response;
using BookWeb.Domain.ResultType;
using MediatR;

namespace BookWeb.Application.Queries;

public class GetBookByIdQuery : IRequest<Result<BookDto>>
{
    public required ClaimsPrincipal? ClaimsPrincipal { get; init; }
    public required string BookId { get; init; }
}