using FilmMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmMS.Infrastructure;

public class ApplicationDbContext : DbContext
{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Film> Films { get; set; }   
}

