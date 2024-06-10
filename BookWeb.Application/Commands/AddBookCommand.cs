using BookWeb.Domain.DTO.Request;
using BookWeb.Domain.ResultType;
using MediatR;

namespace BookWeb.Application.Commands;

public class AddBookCommand : IRequest<Result<EmptyResult>>
{
    public required AddBookRequest Request { get; init; }
}