using System.Security.Claims;
using BookWeb.API.Endpoints.Configuration;
using BookWeb.Application.Queries;
using BookWeb.Domain.Routes;
using BookWeb.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.API.Endpoints.Features.User;

public class GetUserEndpoint : IEndpointBase
{
    public string Group => RouteGroupNames.User;
    public string Route => RouteNames.Get;
    
    public RouteHandlerBuilder MapEndpoint(RouteGroupBuilder builder)
    {
        return builder.MapGet(Route, 
                async (ClaimsPrincipal user, [FromServices] IMediator mediator) =>
                {
                    var result = await mediator.Send(new GetUserQuery
                    {
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
            .RequireAuthorization();
    }
}