using Microsoft.Extensions.DependencyInjection;

namespace BookWeb.Infrastructure.Configuration;

internal static class ServicesInstaller
{
    internal static IServiceCollection InstallServices(this IServiceCollection services)
    {
        return services;
    }
}