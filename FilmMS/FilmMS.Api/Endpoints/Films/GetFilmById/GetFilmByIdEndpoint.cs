using FilmMS.Api.Interfaces;
using FilmMS.Application.Films.GetFilmById;
using MediatR;

namespace FilmMS.Api.Endpoints.Films.GetFilmById;

/// <summary>
/// Endpoint to get film by Film ID
/// </summary>
/// <remarks>
/// - ID - it's the id of the film that you want to get
/// </remarks>
/// <param name="id">The unique identifier of the film you want to retrieve.</param>
/// <returns>
/// - 200 OK: Returns the requested film.
/// - 404 Not Found: If no film with the given ID exists.
/// </returns>
public class GetFilmByIdEndpoint : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/films/{id}", GetFilmByIdHandler).WithTags("Films")
            .WithDescription("Gets the film by id." +
            "You should provide Id of film").WithSummary("Get the film by id.");
    }

    private async Task<IResult> GetFilmByIdHandler(int id,IMediator mediator)
    {

        var query = new GetFilmQuery(id);
        var result = await mediator.Send(query);

        if (result.IsError)
        {
            var errors = result.Errors;
            return Results.BadRequest(errors);
        }
        
        return Results.Ok(result.Value);
    }
}