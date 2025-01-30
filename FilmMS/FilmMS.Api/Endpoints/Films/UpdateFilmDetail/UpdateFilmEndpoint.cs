using ErrorOr;
using FilmMS.Api.Endpoints.Films.Validation;
using FilmMS.Api.Interfaces;
using FilmMS.Application.Films.UpdateFilm;
using FilmMS.Domain.Entities;
using FluentValidation.Results;
using MediatR;

namespace FilmMS.Api.Endpoints.Films.UpdateFilmDetail;


/// <summary>
/// Endpoint for modifying an existing film.
/// The request data is validated in the endpoint handler.
/// </summary>
/// <remarks>
/// - You must provide the updated film data in the request body (JSON format).
/// - The film ID must be included in the URL as a route parameter.
/// </remarks>
public class UpdateFilmEndpoint : IEndpoint
{
    private readonly FilmEndpointValidation _postNewFilmValidation = new();

    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/films/{id}", UpdateFilmHandler)
            .WithTags("Films")
            .WithDescription("Updates a film, you must provide id of film " +
                             "in URL and changed data in body(Type of *Film*")
            .WithSummary("Updates a film.");
    }

    private async Task<IResult> UpdateFilmHandler(int id, Film request, IMediator mediator)
    {
        ValidationResult validationResult = await _postNewFilmValidation.ValidateAsync(request);
        
        if(!validationResult.IsValid)
        {
            return Results.BadRequest(new
            {
                Message = "Validation failed",
                Errors = validationResult.Errors.Select(x => x.ErrorMessage)
            });
        }
        
        request.ReleaseDate = request.ReleaseDate.ToUniversalTime();
        
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