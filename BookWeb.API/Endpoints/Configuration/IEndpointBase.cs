namespace BookWeb.API.Endpoints.Configuration;

public interface IEndpointBase
{
    string Group { get; }
    string Route { get; }
    RouteHandlerBuilder MapEndpoint(RouteGroupBuilder builder);
}