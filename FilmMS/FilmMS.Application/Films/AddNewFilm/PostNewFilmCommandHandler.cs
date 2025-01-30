using ErrorOr;
using FilmMS.Domain.Entities;
using FilmMS.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmMS.Application.Films.AddNewFilm;



public sealed record PostNewFilmCommnad(Film Film) : IRequest<ErrorOr<bool>>; 
    
    
public class PostNewFilmCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<PostNewFilmCommnad, ErrorOr<bool>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;


    public async Task<ErrorOr<bool>> Handle(PostNewFilmCommnad request, CancellationToken cancellationToken)
    {
        try
        {
        await _dbContext.Films.AddAsync(request.Film, cancellationToken);
        
        var result= await _dbContext.SaveChangesAsync(cancellationToken);

        if (result == 0)
        {
            return Error.Failure(
                code: "DATABASE_ERROR",
                description: "No changes were saved to the database. Ensure the data is valid."
            );
        }

        return true;
        
        
        } catch (DbUpdateException dbEx)
        {
            return Error.Failure(
                code: "DB_UPDATE_ERROR",
                description: $"A database update error occurred: {dbEx.Message}"
            );
        }

    }
}