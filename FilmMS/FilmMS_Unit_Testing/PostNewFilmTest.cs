using FilmMS.Application.Films.AddNewFilm;
using FilmMS.Domain.Entities;
using FilmMS.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FilmMS_Unit_Testing;

public class PostNewFilmTest
{
    private ApplicationDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task TestShouldReturnTrueIfFilmAdded()
    {
        using var context = CreateInMemoryDbContext();
        var handler = new PostNewFilmCommandHandler(context);
        var newFilm = new Film { Id = 1,
            Title = "Inception",
            Description = "Sci-fi movie",
            ReleaseDate = DateTime.Now,
            Director = "Director",
            Genre = "Action",
            Rating = 2.2};
        
        var command = new PostNewFilmCommnad(newFilm);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsError); 
        Assert.True(result.Value); 
    }
}