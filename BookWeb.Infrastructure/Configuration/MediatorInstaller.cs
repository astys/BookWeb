using Microsoft.Extensions.DependencyInjection;

namespace BookWeb.Infrastructure.Configuration;

internal static class MediatorInstaller
{
    internal static IServiceCollection InstallMediator(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(MediatorEntryPoint).Assembly);
        });

        return services;
    }
}