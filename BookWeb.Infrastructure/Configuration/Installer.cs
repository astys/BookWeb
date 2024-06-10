using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookWeb.Infrastructure.Configuration;

public static class Installer
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection serviceCollection, 
        IConfiguration configuration,
        Assembly assembly)
    {
        return serviceCollection
            .InstallSwagger()
            .InstallDatabase(configuration, assembly)
            .InstallRepositories()
            .InstallServices()
            .InstallAuth()
            .InstallCors()
            .InstallMediator();
    }
}