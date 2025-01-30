using FilmMS.Application.Films.DeleteFilm;
using FilmMS.Domain.Entities;
using FilmMS.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FilmMS_Unit_Testing;

public class DeleteFilmTest
{
    private ApplicationDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }


    [Fact]
    public async Task TestDeleteFilm_ShouldDeleteFilm()
    {
        using var context = CreateInMemoryDbContext();
        
        var testFilm = new Film
        {
            Id = 1,
            Title = "Test Title",
            Description = "Test Description",
            Director = "Test Director",
            Genre = "Test Genre",
            Rating = 5,
            ReleaseDate = new DateTime(2020, 01, 01),
        };
        
        await context.Films.AddAsync(testFilm);
        await context.SaveChangesAsync(); 
        
        var handler = new DeleteFilmCommandHandler(context);
        var command = new DeleteFilmCommand(1);
        
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsError);
        Assert.True(result.Value);
        
        Assert.Null(await context.Films.FindAsync(1)); 
    }
    
    [Fact]
    public async Task Handle_Should_Return_NonFountFilm()
    {
        using var context = CreateInMemoryDbContext();
        var handler = new DeleteFilmCommandHandler(context);
        var command = new DeleteFilmCommand(99);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsError); 
        Assert.Single(result.Errors); 
        Assert.Equal("Film.NotFound", result.Errors[0].Code);
    }
}