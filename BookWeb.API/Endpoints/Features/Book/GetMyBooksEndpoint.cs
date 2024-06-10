using System.Security.Claims;
using BookWeb.API.Endpoints.Configuration;
using BookWeb.Application.Queries;
using BookWeb.Domain.DTO.QueryParams;
using BookWeb.Domain.Routes;
using BookWeb.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.API.Endpoints.Features.Book;

public class GetMyBooksEndpoint : IEndpointBase
{
    public string Group => RouteGroupNames.Book;
    public string Route => RouteNames.GetMine;
    
    public RouteHandlerBuilder MapEndpoint(RouteGroupBuilder builder)
    {
        return builder.MapGet(Route, 
                async (
                    [FromQuery] int pageNumber, 
                    [FromQuery] int pageSize, 
                    [FromQuery] string title,
                    ClaimsPrincipal user,
                    [FromServices] IMediator mediator) =>
                {
                    var result = await mediator.Send(new GetMyBooksQuery
                    {
                        PaginationParameters = new PaginationParameters(pageSize, pageNumber),
                        Title = title,
                        ClaimsPrincipal = user
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