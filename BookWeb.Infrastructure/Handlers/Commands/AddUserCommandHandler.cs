using BookWeb.Application.Commands;
using BookWeb.Application.Repositories;
using BookWeb.Domain.DatabaseModels;
using BookWeb.Domain.ResultType;
using BookWeb.Domain.ResultType.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BookWeb.Infrastructure.Handlers.Commands;

public class AddUserCommandHandler(
    UserManager<ApplicationUser> userManager, 
    IApplicationUserGenreRepository userGenreRepository)
    : IRequestHandler<AddUserCommand, Result<EmptyResult>>
{
    public async Task<Result<EmptyResult>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var userToCreate = new ApplicationUser
        {
            Name = request.Request.Name,
            UserName = request.Request.Email,
            Email = request.Request.Email
        };
        var result = await userManager.CreateAsync(userToCreate, request.Request.Password);
        
        var userId = await userManager.GetUserIdAsync(userToCreate);

        if (!result.Succeeded)
        {
            return Result<EmptyResult>.Failure(GeneralError.New(result.Errors.Select(x => x.Description).FirstOrDefault() ?? string.Empty));
        }
            
        foreach (var genreId in request.Request.Genres)
        {
            var userGenre = new ApplicationUserGenre
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                GenreId = genreId
            };

            await userGenreRepository.AddAsync(userGenre);
        }

        await userGenreRepository.SaveChangesAsync();

        return Result<EmptyResult>.Success();

    }
}