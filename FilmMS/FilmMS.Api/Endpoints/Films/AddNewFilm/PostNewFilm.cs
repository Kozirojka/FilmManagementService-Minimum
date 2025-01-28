using ErrorOr;
using FilmMS.Api.Interfaces;
using FilmMS.Application.Films.AddNewFilm;
using FilmMS.Domain.Entities;
using MediatR;

namespace FilmMS.Api.Endpoints.Films.AddNewFilm;


public class PostNewFilm : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/films", POstNewFilmHandler);
    }

    private async Task<IResult> POstNewFilmHandler(Film film, IMediator mediator)
    {

        var command = new PostNewFilmCommnad(film);
        var result = await mediator.Send(command);


        if (result.IsError)
        {
            return Results.BadRequest(result.Errors);
        }
        
        return Results.Ok(result.Value);
    }
}