using BookWeb.API.Endpoints.Configuration;
using BookWeb.Application.Queries;
using BookWeb.Domain.DTO.QueryParams;
using BookWeb.Domain.Routes;
using BookWeb.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.API.Endpoints.Features.Book;

public class GetTopBooksEndpoint : IEndpointBase
{
    public string Group => RouteGroupNames.Book;
    public string Route => RouteNames.GetTop;
    
    public RouteHandlerBuilder MapEndpoint(RouteGroupBuilder builder)
    {
        return builder.MapGet(Route, 
                async (
                    [FromServices] IMediator mediator) =>
                {
                    var result = await mediator.Send(new GetTopBooksQuery());

                    if (result.IsSuccess)
                    {
                        return Results.Ok(result.Value);
                    }

                    return result.IsGeneralError() 
                        ? Results.UnprocessableEntity() 
                        : Results.BadRequest();
                })
            .AllowAnonymous();
    }
}