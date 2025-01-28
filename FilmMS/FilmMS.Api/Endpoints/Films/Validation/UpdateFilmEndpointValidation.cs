using FilmMS.Domain.Entities;
using FluentValidation;

namespace FilmMS.Api.Endpoints.Films.Validation;

public class UpdateFilmEndpointValidation : AbstractValidator<Film>
{
    public UpdateFilmEndpointValidation()
    {
        RuleFor(f => f.Title).NotEmpty().WithMessage("Title is required.");
        
        RuleFor(f => f.Description).NotEmpty().WithMessage("Description is required.");
        
        RuleFor(f => f.ReleaseDate)
            .LessThan(DateTime.Today)
            .WithMessage("ReleaseDate is required.");
        
        RuleFor(f => f.Genre).NotEmpty().WithMessage("Genre is required.");
        
        RuleFor(f => f.Rating).LessThan(10).WithMessage("Rating must be less than 10")
            .GreaterThan(0).WithMessage("Rating must be greater than 0");
    }
}