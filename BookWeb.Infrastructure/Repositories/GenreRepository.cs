using BookWeb.Application.Database;
using BookWeb.Application.Repositories;
using BookWeb.Domain.DatabaseModels;

namespace BookWeb.Infrastructure.Repositories;

public class GenreRepository(DatabaseContext databaseContext)
    : GenericRepository<Genre>(databaseContext), IGenreRepository;