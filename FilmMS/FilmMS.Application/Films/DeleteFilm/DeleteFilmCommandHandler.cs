using ErrorOr;
using FilmMS.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FilmMS.Application.Films.DeleteFilm;

public sealed record DeleteFilmCommand(int Id) : IRequest<ErrorOr<bool>>;

public class DeleteFilmCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<DeleteFilmCommand, ErrorOr<bool>>
{
    
    
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<ErrorOr<bool>> Handle(DeleteFilmCommand request, CancellationToken cancellationToken)
    {   
        var film = await _dbContext.Films
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        if (film is null)
        {
            return Error.NotFound(
                code: "Film.NotFound",
                description: $"Film with Id {request.Id} не знайдено."
            );
        }
        
        _dbContext.Films.Remove(film);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;

    }
}   