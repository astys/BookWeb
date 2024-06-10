using BookWeb.Application.Database;
using BookWeb.Application.Repositories;
using BookWeb.Domain.DatabaseModels;

namespace BookWeb.Infrastructure.Repositories;

public class BookReviewRepository(DatabaseContext databaseContext)
    : GenericRepository<BookReview>(databaseContext), IBookReviewRepository;