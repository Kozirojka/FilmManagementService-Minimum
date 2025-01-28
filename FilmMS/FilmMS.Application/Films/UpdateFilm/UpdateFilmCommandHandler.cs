using ErrorOr;
using FilmMS.Domain.Entities;
using FilmMS.Infrastructure;
using MediatR;

namespace FilmMS.Application.Films.UpdateFilm;

public sealed record UpdateFilmCommand(Film Film, int Id) : IRequest<ErrorOr<bool>>;

public class UpdateFilmCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<UpdateFilmCommand, ErrorOr<bool>>
{
    
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<ErrorOr<bool>> Handle(UpdateFilmCommand request, CancellationToken cancellationToken)
    {
        var film =  _dbContext.Films.FindAsync(request.Id, cancellationToken).Result;
        
        film?.Update(request.Film);
        
        var result =  await _dbContext.SaveChangesAsync(cancellationToken);

        if (result < 0)
        {
            return Error.Failure("Film.Failure","Dot not possible to update the film");
        }

        return true;
    }
}