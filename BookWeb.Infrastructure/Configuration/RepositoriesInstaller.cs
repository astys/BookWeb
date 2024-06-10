using BookWeb.Application.Repositories;
using BookWeb.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BookWeb.Infrastructure.Configuration;

internal static class RepositoriesInstaller
{
    internal static IServiceCollection InstallRepositories(this IServiceCollection services)
    {
        services.AddScoped<IApplicationUserGenreRepository, ApplicationUserGenreRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookReviewRepository, BookReviewRepository>();
        return services;
    }
}