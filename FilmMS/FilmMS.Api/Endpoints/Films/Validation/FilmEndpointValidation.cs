using FilmMS.Domain.Entities;
using FluentValidation;

namespace FilmMS.Api.Endpoints.Films.Validation;

// Validates the Film entity before processing
public class FilmEndpointValidation : AbstractValidator<Film>
{
    public FilmEndpointValidation()
    {
        RuleFor(f => f.Title).NotEmpty().WithMessage("Title is required.");
        
        RuleFor(f => f.Description).NotEmpty().WithMessage("Description is required.");
        
        RuleFor(f => f.ReleaseDate)
            .LessThan(DateTime.Today)
            .WithMessage("Release should be less than today.");
        
        RuleFor(f => f.Genre).NotEmpty().WithMessage("Genre is required.");
        
        // Rating must be between 1 and 10
        RuleFor(f => f.Rating).LessThanOrEqualTo(10).WithMessage("Rating must be less than 10")
            .GreaterThanOrEqualTo(1).WithMessage("Rating must be greater than 0");
    }
}