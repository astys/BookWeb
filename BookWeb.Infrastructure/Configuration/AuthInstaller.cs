using BookWeb.Application.Database;
using BookWeb.Domain.DatabaseModels;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.Extensions.DependencyInjection;

namespace BookWeb.Infrastructure.Configuration;

internal static class AuthInstaller
{
    internal static IServiceCollection InstallAuth(this IServiceCollection services)
    {
        services
            .AddAuthorization()
            .AddIdentityApiEndpoints<ApplicationUser>()
            .AddEntityFrameworkStores<DatabaseContext>();
        
        services.ConfigureAll<BearerTokenOptions>(opt =>
        {
            opt.BearerTokenExpiration = TimeSpan.FromDays(7);
            opt.RefreshTokenExpiration = TimeSpan.FromDays(365);
        });

        return services;
    }
}