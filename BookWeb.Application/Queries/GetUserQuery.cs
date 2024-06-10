using System.Security.Claims;
using BookWeb.Domain.DTO.Response;
using BookWeb.Domain.ResultType;
using MediatR;

namespace BookWeb.Application.Queries;

public class GetUserQuery : IRequest<Result<GetUserResponse>>
{
    public required ClaimsPrincipal ClaimsPrincipal { get; init; }
}