using ErrorOr;
using FilmMS.Domain.Entities;
using FilmMS.Infrastructure;
using MediatR;

namespace FilmMS.Application.Films.AddNewFilm;



public sealed record PostNewFilmCommnad(Film Film) : IRequest<ErrorOr<bool>>; 
    
    
public class PostNewFilmCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<PostNewFilmCommnad, ErrorOr<bool>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;


    public async Task<ErrorOr<bool>> Handle(PostNewFilmCommnad request, CancellationToken cancellationToken)
    {
        await _dbContext.Films.AddAsync(request.Film, cancellationToken);
        
        var result= await _dbContext.SaveChangesAsync(cancellationToken);

        if (result < 0)
        {
            return Error.Failure();
        }

        return true;
    }
}