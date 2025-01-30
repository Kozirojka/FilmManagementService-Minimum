using FilmMS.Application.Films.UpdateFilm;
using FilmMS.Domain.Entities;
using FilmMS.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FilmMS_Unit_Testing;

public class UpdateFilmTest
{
    private ApplicationDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    /// <summary>
    /// Assumes that the data passed from the controller
    /// are already validated and absolutely good for MediatR handler
    /// </summary>
    [Fact]
    public async Task UpdateFilmTest_WithSuccessfullyUpdatedFilm()
    {
        using var context = CreateInMemoryDbContext();

        var oldFilm = new Film
        {
            Id = 1,
            Title = "Old Film",
            Director = "Film Director",
            Genre = "Film Genre",
            ReleaseDate = new DateTime(2021, 12, 10),
            Description = "Old Film Description",
            Rating = 5,
        };
        
        var newFilm = new Film
        {
            Title = "New Film",
            Director = "Hello world",
            Genre = "Rock",
            ReleaseDate = new DateTime(2021, 12, 10),
            Description = "New Film Description",
            Rating = 5,
        };
        
         await context.Films.AddAsync(oldFilm);
         await context.SaveChangesAsync();

         var handler = new UpdateFilmCommandHandler(context);
         var command = new UpdateFilmCommand(newFilm, 1);
         
         var result = await handler.Handle(command, CancellationToken.None);
         
         Assert.False(result.IsError); 
         Assert.True(result.Value); 

         var updatedFilm = await context.Films.FindAsync(1);
         Assert.NotNull(updatedFilm);
         Assert.Equal("New Film", updatedFilm.Title); 
         Assert.Equal("Hello world", updatedFilm.Director);
         
         
    }
}