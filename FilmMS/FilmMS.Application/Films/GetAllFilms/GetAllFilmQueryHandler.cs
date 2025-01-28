using ErrorOr;
using FilmMS.Domain.Entities;
using FilmMS.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmMS.Application.Films.GetAllFilms;

public sealed record GetAllFilmsQuery : IRequest<ErrorOr<List<Film>>>;


public class GetAllFilmQueryHandler : IRequestHandler<GetAllFilmsQuery, ErrorOr<List<Film>>>
{
    
    private readonly ApplicationDbContext _context;

    public GetAllFilmQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<List<Film>>> Handle(GetAllFilmsQuery request, CancellationToken cancellationToken)
    {
        var films = await _context.Films.ToListAsync(cancellationToken);

        if (films is null || !films.Any())
        {
            return Error.NotFound("Films.NotFound", "No films were found.");
        }

        return films;
    }
}