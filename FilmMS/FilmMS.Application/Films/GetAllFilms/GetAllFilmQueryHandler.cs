using ErrorOr;
using FilmMS.Domain.Entities;
using FilmMS.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmMS.Application.Films.GetAllFilms;

public sealed record GetAllFilmsQuery : IRequest<ErrorOr<List<Film>>>;


public class GetAllFilmQueryHandler(ApplicationDbContext context)
    : IRequestHandler<GetAllFilmsQuery, ErrorOr<List<Film>>>
{
    public async Task<ErrorOr<List<Film>>> Handle(GetAllFilmsQuery request, CancellationToken cancellationToken)
    {
        var films = await context.Films.ToListAsync(cancellationToken);

        
        return films;
    }
}