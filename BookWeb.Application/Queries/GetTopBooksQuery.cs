using BookWeb.Domain.DTO.Response;
using BookWeb.Domain.ResultType;
using MediatR;

namespace BookWeb.Application.Queries;

public class GetTopBooksQuery : IRequest<Result<GetTopBooksResponse>>;