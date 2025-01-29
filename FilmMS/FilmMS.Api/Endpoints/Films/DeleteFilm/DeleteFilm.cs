using FilmMS.Api.Interfaces;
using FilmMS.Application.Films.DeleteFilm;
using FilmMS.Domain.Entities;
using MediatR;

namespace FilmMS.Api.Endpoints.Films.DeleteFilm;

public class DeleteFilm : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDelete("/films/{id}", HandleDeletion);
    }

    private async Task<IResult> HandleDeletion(IMediator mediator, int id)
    {
        var command = new DeleteFilmCommand(id);
        
        var result = await mediator.Send(command);
        
        if (result.IsError)
        {
            return Results.NotFound(result.Errors);
        }

        return Results.Ok(result);
    }
}