using BookWeb.API.Endpoints.Configuration;
using BookWeb.Application.Queries;
using BookWeb.Domain.Routes;
using BookWeb.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.API.Endpoints.Features.Genre;

public class GetAllGenresEndpoint : IEndpointBase
{
    public string Group => RouteGroupNames.Genre;
    public string Route => RouteNames.GetAll;
    
    public RouteHandlerBuilder MapEndpoint(RouteGroupBuilder builder)
    {
        return builder.MapGet(Route, 
                async ([FromServices] IMediator mediator) =>
                {
                    var result = await mediator.Send(new GetAllGenresQuery());

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