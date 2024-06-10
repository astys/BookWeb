using System.Security.Claims;
using BookWeb.Domain.DTO.QueryParams;
using BookWeb.Domain.DTO.Response;
using BookWeb.Domain.ResultType;
using MediatR;

namespace BookWeb.Application.Queries;

public class GetMyBooksQuery : IRequest<Result<GetBooksResponse>>
{
    public required string Title { get; init; } = string.Empty;
    public required PaginationParameters PaginationParameters { get; init; }
    public required ClaimsPrincipal ClaimsPrincipal { get; init; }
}