using FilmMS.Api.Interfaces;
using FilmMS.Application.Films.GetAllFilms;
using FilmMS.Domain.Entities;
using MediatR;

namespace FilmMS.Api.Endpoints.Films.GetAllFilms;

/// <summary>
/// Endpoint to get the list of fims
/// </summary>
/// <returns>
/// Return a list of films TYPE: List<Film>
/// </returns>
public class GetAllFilmsEndpoint : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/films", GetAllFilmsHandlerAsync)
            .WithTags("Films")
            .WithDescription("Get all films")
            .WithSummary("Get all films")
            .Produces<List<Film>>(StatusCodes.Status200OK) ;
    }

    private async Task<IResult> GetAllFilmsHandlerAsync(HttpContext context, IMediator mediator)
    {

        var query = new GetAllFilmsQuery();
        var result = await mediator.Send(query);

        if (result.IsError)
        {
            return Results.Ok(new List<Film>());
        }
        
        return Results.Ok(result.Value);
    }
}