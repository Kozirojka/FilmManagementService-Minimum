using ErrorOr;
using FilmMS.Application.Films.GetFilmById;
using FilmMS.Domain.Entities;
using FilmMS.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FilmMS_Unit_Testing;

    public class GetFilmByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_WithExistingFilm_ReturnsFilm()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            await using (var context = new ApplicationDbContext(options))
            {
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
                context.Films.Add(testFilm);
                await context.SaveChangesAsync();

                var queryHandler = new GetFilmByIdQueryHandler(context);

                var result = await queryHandler.Handle(new GetFilmQuery(1), default);
                
                Assert.False(result.IsError, "Expected a valid Film, but got an Error");
                Assert.NotNull(result.Value);
                Assert.Equal(testFilm.Id, result.Value.Id);
                Assert.Equal(testFilm.Title, result.Value.Title);
            }
        }
        
        
        [Fact]
        public async Task Handle_WithNonExistingFilm_ReturnsNotFoundError()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GetFilmByIdQueryHandler_NonExistingFilm")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var queryHandler = new GetFilmByIdQueryHandler(context);

                var result = await queryHandler.Handle(new GetFilmQuery(999), default);

                Assert.True(result.IsError, "Expected a valid Film, but got an Error");
                Assert.Contains(result.Errors, e => e.Type == ErrorType.NotFound);
            }
        }
}