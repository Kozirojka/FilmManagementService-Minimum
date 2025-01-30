using FilmMS.Application.Films.GetAllFilms;
using FilmMS.Domain.Entities;
using FilmMS.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FilmMS_Unit_Testing;

public class GetAllFilmsTest
{
    private ApplicationDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }


    [Fact]
    public async Task GetAllFilmsTest_ShouldReturnAlistFilms()
    {
        using var context = CreateInMemoryDbContext();

        var testList = new List<Film>
        {
            new Film
            {
                Id = 1,
                Title = "Test Title",
                Description = "Test Description",
                Director = "Test Director",
                Genre = "Test Genre",
                Rating = 5,
                ReleaseDate = new DateTime(2020, 01, 01),
            },
            new Film
            {
                Id = 2,
                Title = "Second Test Title",
                Description = "Test Description",
                Director = "Test Director",
                Genre = "Test Genre",
                Rating = 5,
                ReleaseDate = new DateTime(2020, 01, 01),
            },
            new Film
            {
                Id = 3,
                Title = "Five Test Title",
                Description = "Test Description",
                Director = "Test Director",
                Genre = "Test Genre",
                Rating = 5,
                ReleaseDate = new DateTime(2020, 01, 01),
            }
        };
        
        await context.Films.AddRangeAsync(testList);
        await context.SaveChangesAsync();
        var command = new GetAllFilmsQuery();

        var handler = new GetAllFilmQueryHandler(context);

        var result = await handler.Handle(command, CancellationToken.None);

        
        Assert.Equal(testList.Count, result.Value.Count);
        Assert.Equal(testList[0].Id, result.Value[0].Id);
    }
}