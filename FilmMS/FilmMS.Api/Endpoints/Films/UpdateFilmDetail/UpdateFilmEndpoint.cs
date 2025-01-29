using ErrorOr;
using FilmMS.Api.Endpoints.Films.Validation;
using FilmMS.Api.Interfaces;
using FilmMS.Application.Films.UpdateFilm;
using FilmMS.Domain.Entities;
using FluentValidation.Results;
using MediatR;

namespace FilmMS.Api.Endpoints.Films.UpdateFilmDetail;

public class UpdateFilmEndpoint : IEndpoint
{
    private readonly FilmEndpointValidation _postNewFilmValidation = new();

    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPut("/films/{id}", UpdateFilmHandler).WithTags("Films");
    }

    private async Task<IResult> UpdateFilmHandler(int id, Film request, IMediator mediator)
    {
        ValidationResult validationResult = await _postNewFilmValidation.ValidateAsync(request);
        
        if(!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }
        
        if (request.ReleaseDate.Kind == DateTimeKind.Unspecified)
        {
            request.ReleaseDate = DateTime.SpecifyKind(request.ReleaseDate, DateTimeKind.Utc);
        }
        else
        {
            request.ReleaseDate = request.ReleaseDate.ToUniversalTime();
        }
        
        
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