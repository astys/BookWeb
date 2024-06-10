using BookWeb.Application.Queries;
using BookWeb.Application.Repositories;
using BookWeb.Domain.DatabaseModels;
using BookWeb.Domain.DTO.Response;
using BookWeb.Domain.ResultType;
using BookWeb.Domain.ResultType.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Infrastructure.Handlers.Queries;

public class GetUserQueryHandler(
    UserManager<ApplicationUser> userManager, 
    IBookReviewRepository bookReviewRepository)
    : IRequestHandler<GetUserQuery, Result<GetUserResponse>>
{
    public async Task<Result<GetUserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.GetUserAsync(request.ClaimsPrincipal);

        if (user is null)
        {
            return Result<GetUserResponse>.Failure(NotFoundError.New(nameof(ApplicationUser)));
        }
        
        var reviewsCount = await bookReviewRepository.GetAll()
            .Where(x => x.UserId == user.Id)
            .CountAsync(cancellationToken);
        var response = new GetUserResponse
        {
            EmailAddress = user.Email!,
            Name = user.Name!,
            ReviewsCount = reviewsCount,
            FavouriteGenres = user.UserGenres?.Select(x => x.Genre?.Name ?? string.Empty) ?? []
        };

        return Result<GetUserResponse>.Success(response);
    }
}