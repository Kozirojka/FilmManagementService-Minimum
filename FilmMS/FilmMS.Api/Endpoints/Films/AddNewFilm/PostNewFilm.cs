using ErrorOr;
using FilmMS.Api.Endpoints.Films.UpdateFilmDetail;
using FilmMS.Api.Endpoints.Films.Validation;
using FilmMS.Api.Interfaces;
using FilmMS.Application.Films.AddNewFilm;
using FilmMS.Domain.Entities;
using FluentValidation.Results;
using MediatR;

namespace FilmMS.Api.Endpoints.Films.AddNewFilm;


/// <summary>
/// Api endpoint for adding film to database
/// Validates the input, ensures correct date format, and sends the request to MediatR.
/// </summary>
public class PostNewFilm : IEndpoint
{
    private readonly FilmEndpointValidation _postNewFilmValidation = new();
    
    
    public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/films", PostNewFilmHandler)
            .WithTags("Films") 
            .WithSummary("Add a new film")
            .WithDescription("Adds a favorite film to the database.") 
            .Produces<Film>(StatusCodes.Status200OK) 
            .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest);
    }
    
    private async Task<IResult> PostNewFilmHandler(Film film, IMediator mediator)
    {

        ValidationResult validationResult = await _postNewFilmValidation.ValidateAsync(film);
        
        if(!validationResult.IsValid)
        {
            return Results.BadRequest(new
            {
                Message = "Validation failed",
                Errors = validationResult.Errors.Select(x => x.ErrorMessage)
            });
        }

        
        film.ReleaseDate = film.ReleaseDate.ToUniversalTime();
        
        var command = new PostNewFilmCommnad(film);
        var result = await mediator.Send(command);


        if (result.IsError)
        {
            return Results.BadRequest(result.Errors);
        }
        
        return Results.Ok(result.Value);
    }
}