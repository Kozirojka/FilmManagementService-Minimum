using ErrorOr;
using FilmMS.Api.Endpoints.Films.UpdateFilmDetail;
using FilmMS.Api.Endpoints.Films.Validation;
using FilmMS.Api.Interfaces;
using FilmMS.Application.Films.AddNewFilm;
using FilmMS.Domain.Entities;
using FluentValidation.Results;
using MediatR;

namespace FilmMS.Api.Endpoints.Films.AddNewFilm;


public class PostNewFilm : IEndpoint
{
    private readonly UpdateFilmEndpointValidation _postNewFilmValidation = new();
    
    
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/films", POstNewFilmHandler).WithTags("Films");
    }

    private async Task<IResult> POstNewFilmHandler(Film film, IMediator mediator)
    {

        ValidationResult validationResult = await _postNewFilmValidation.ValidateAsync(film);
        
        if(!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }
        
        var command = new PostNewFilmCommnad(film);
        var result = await mediator.Send(command);


        if (result.IsError)
        {
            return Results.BadRequest(result.Errors);
        }
        
        return Results.Ok(result.Value);
    }
}