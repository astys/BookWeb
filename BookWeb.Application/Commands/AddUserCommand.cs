using BookWeb.Domain.DTO.Request;
using BookWeb.Domain.ResultType;
using MediatR;

namespace BookWeb.Application.Commands;

public class AddUserCommand : IRequest<Result<EmptyResult>>
{
    public required AddUserRequest Request { get; init; }
}