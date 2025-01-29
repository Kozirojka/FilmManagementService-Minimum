using System.Net.Mime;

namespace FilmMS.Domain.Entities;

public class Film
{
    public int? Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Description { get; set; }
    public double Rating { get; set; }

    public void Update(Film film)
    {
        this.Title = film.Title;
        this.Genre = film.Genre;
        this.Director = film.Director;
        this.ReleaseDate = film.ReleaseDate;
        this.Description = film.Description;
        this.Rating = film.Rating;
    }
}