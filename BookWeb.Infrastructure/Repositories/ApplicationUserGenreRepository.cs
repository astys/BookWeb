using BookWeb.Application.Database;
using BookWeb.Application.Repositories;
using BookWeb.Domain.DatabaseModels;

namespace BookWeb.Infrastructure.Repositories;

internal class ApplicationUserGenreRepository(DatabaseContext databaseContext)
    : GenericRepository<ApplicationUserGenre>(databaseContext), IApplicationUserGenreRepository;