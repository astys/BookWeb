using Microsoft.Extensions.DependencyInjection;

namespace BookWeb.Infrastructure.Configuration;

internal static class CorsInstaller
{
    internal static IServiceCollection InstallCors(this IServiceCollection services)
    {
        services.AddCors(p =>
        {
            p.AddDefaultPolicy(c =>
            {
                c.AllowAnyOrigin();
                c.AllowAnyMethod();
                c.AllowAnyHeader();
            });
        });

        return services;
    }
}