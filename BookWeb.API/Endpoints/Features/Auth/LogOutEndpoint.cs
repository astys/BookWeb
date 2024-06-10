using BookWeb.API.Endpoints.Configuration;
using BookWeb.Domain.DatabaseModels;
using BookWeb.Domain.Routes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.API.Endpoints.Features.Auth;

public sealed class LogOutEndpoint : IEndpointBase
{
    public string Group => RouteGroupNames.Auth;
    public string Route => RouteNames.LogOut;
    public RouteHandlerBuilder MapEndpoint(RouteGroupBuilder builder)
    {
        return builder.MapPost(Route, 
            async ([FromServices] SignInManager<ApplicationUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return Results.Ok();
            })
            .RequireAuthorization();
    }
}