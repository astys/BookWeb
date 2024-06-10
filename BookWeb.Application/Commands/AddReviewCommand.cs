using System.Security.Claims;
using BookWeb.Domain.DTO.Request;
using BookWeb.Domain.ResultType;
using MediatR;

namespace BookWeb.Application.Commands;

public class AddReviewCommand : IRequest<Result<EmptyResult>>
{
    public required ClaimsPrincipal ClaimsPrincipal { get; init; }
    public required AddReviewRequest Request { get; init; }
}