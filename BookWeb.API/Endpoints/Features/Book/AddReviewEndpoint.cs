using System.Security.Claims;
using BookWeb.API.Endpoints.Configuration;
using BookWeb.Application.Commands;
using BookWeb.Domain.DTO.Request;
using BookWeb.Domain.Routes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.API.Endpoints.Features.Book;

public sealed class AddReviewEndpoint : IEndpointBase
{
    public string Group => RouteGroupNames.Book;
    public string Route => RouteNames.AddReview;
    
    public RouteHandlerBuilder MapEndpoint(RouteGroupBuilder builder)
    {
        return builder.MapPost(Route,
                async (
                    [FromBody] AddReviewRequest payload,
                    ClaimsPrincipal user,
                    [FromServices] IMediator mediator) =>
                {
                    var command = new AddReviewCommand()
                    {
                        Request = payload,
                        ClaimsPrincipal = user
                    };

                    await mediator.Send(command);

                    return Results.Ok();
                })
            .RequireAuthorization();
    }
}