using ErrorOr;
using FilmMS.Domain.Entities;
using FilmMS.Infrastructure;
using MediatR;

namespace FilmMS.Application.Films.GetFilmById;


public sealed record GetFilmQuery(int Id) : IRequest<ErrorOr<Film>>;



public class GetFilmByIdQueryHandler(ApplicationDbContext dbContext) : IRequestHandler<GetFilmQuery, ErrorOr<Film>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<ErrorOr<Film>> Handle(GetFilmQuery query, CancellationToken cancellationToken)
    {
        var result = _dbContext.Films.SingleOrDefault(f => f.Id == query.Id);


        if (result == null)
        {
            return Error.NotFound(
                code: "Film.NotFound",
                description: $"No film with id: {query.Id} was found"
            );
        }

        return result;

    }
    
}