using System.Reflection;
using BookWeb.Application.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookWeb.Infrastructure.Configuration;

internal static class DatabaseInstaller
{
    internal static IServiceCollection InstallDatabase(
        this IServiceCollection services, 
        IConfiguration configuration,
        Assembly assembly)
    {
        var connectionString = configuration.GetConnectionString("Sqlite");
        services.AddDbContext<DatabaseContext>(options
            => options
                .UseLazyLoadingProxies()
                .UseSqlite(connectionString, 
                    c => c.MigrationsAssembly(assembly.FullName)));
        return services;
    } 
}