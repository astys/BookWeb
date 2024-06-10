using BookWeb.API.Endpoints.Configuration;
using BookWeb.Application.Commands;
using BookWeb.Application.Queries;
using BookWeb.Domain.DTO.Request;
using BookWeb.Domain.Routes;
using BookWeb.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.API.Endpoints.Features.User;

public class AddUserEndpoint : IEndpointBase
{
    public string Group => RouteGroupNames.User;
    public string Route => RouteNames.Add;
    
    public RouteHandlerBuilder MapEndpoint(RouteGroupBuilder builder)
    {
        return builder.MapPost(Route, 
                async ([FromBody] AddUserRequest request, [FromServices] IMediator mediator) =>
                {
                    var result = await mediator.Send(new AddUserCommand
                    {
                        Request = request
                    });

                    return result.IsSuccess 
                        ? Results.Ok() 
                        : Results.UnprocessableEntity(result.Error?.Description);
                })
            .AllowAnonymous();
    }
}