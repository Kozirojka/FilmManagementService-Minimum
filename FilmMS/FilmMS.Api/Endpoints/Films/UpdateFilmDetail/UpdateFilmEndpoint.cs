using ErrorOr;
using FilmMS.Api.Interfaces;
using FilmMS.Application.Films.UpdateFilm;
using FilmMS.Domain.Entities;
using MediatR;

namespace FilmMS.Api.Endpoints.Films.UpdateFilmDetail;

public class UpdateFilmEndpoint : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/films/{id}", UpdateFilmHandler);
    }

    private async Task<IResult> UpdateFilmHandler(int id, Film request, IMediator mediator)
    {
        var command = new UpdateFilmCommand(request, id);
        
        var result = await mediator.Send(command);

        if (result.IsError)
        {
            var errors = result.Errors;
            return Results.BadRequest(errors);
        }
        
        return Results.Ok(result.Value);
    }
}