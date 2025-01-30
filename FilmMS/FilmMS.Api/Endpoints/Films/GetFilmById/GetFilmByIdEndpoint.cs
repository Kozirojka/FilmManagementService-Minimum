using FilmMS.Api.Interfaces;
using FilmMS.Application.Films.GetFilmById;
using MediatR;

namespace FilmMS.Api.Endpoints.Films.GetFilmById;



public class GetFilmByIdEndpoint : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/films/{id}", GetFilmByIdHandler).WithTags("Films").WithDescription("Gets the film by id");
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