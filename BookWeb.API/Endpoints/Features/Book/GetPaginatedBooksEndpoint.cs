using BookWeb.API.Endpoints.Configuration;
using BookWeb.Application.Queries;
using BookWeb.Domain.DTO.QueryParams;
using BookWeb.Domain.Routes;
using BookWeb.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.API.Endpoints.Features.Book;

public class GetPaginatedBooksEndpoint : IEndpointBase
{
    public string Group => RouteGroupNames.Book;
    public string Route => RouteNames.GetAll;
    
    public RouteHandlerBuilder MapEndpoint(RouteGroupBuilder builder)
    {
        return builder.MapGet(Route, 
                async (
                    [FromQuery] int pageNumber, 
                    [FromQuery] int pageSize, 
                    [FromQuery] string title,
                    [FromServices] IMediator mediator) =>
                {
                    var result = await mediator.Send(new GetPaginatedBooksQuery
                    {
                        PaginationParameters = new PaginationParameters(pageSize, pageNumber),
                        Title = title
                    });

                    if (result.IsSuccess)
                    {
                        return Results.Ok(result.Value);
                    }
                    
                    if (result.IsNotFound())
                    {
                        return Results.NotFound();
                    }

                    return result.IsGeneralError() 
                        ? Results.UnprocessableEntity() 
                        : Results.BadRequest();
                })
            .AllowAnonymous();
    }
}