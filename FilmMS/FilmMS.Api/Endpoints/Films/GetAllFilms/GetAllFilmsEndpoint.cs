using FilmMS.Api.Interfaces;
using FilmMS.Application.Films.GetAllFilms;
using MediatR;

namespace FilmMS.Api.Endpoints.Films.GetAllFilms;


public class GetAllFilmsEndpoint : IEndpoint
{
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/films", GetAllFilmsHandlerAsync)
            .WithTags("Films")
            .WithDescription("Get all films");
    }

    private async Task<IResult> GetAllFilmsHandlerAsync(HttpContext context, IMediator _mediator)
    {

        var query = new GetAllFilmsQuery();
        var result = await _mediator.Send(query);

        if (result.IsError)
        {
            var firstError = result.FirstError;
            return Results.Problem(
                detail: firstError.Description,
                title: firstError.Code,
                statusCode: StatusCodes.Status500InternalServerError
            );
        }
        
        return Results.Ok(result.Value);
    }
}