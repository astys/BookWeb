using BookWeb.Application.Database;
using BookWeb.Application.Repositories;
using BookWeb.Domain.DatabaseModels;

namespace BookWeb.Infrastructure.Repositories;

public class BookRepository(DatabaseContext databaseContext)
    : GenericRepository<Book>(databaseContext), IBookRepository;