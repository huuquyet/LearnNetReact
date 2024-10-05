using Microsoft.EntityFrameworkCore;
using LearnNetReact.Models;

namespace LearnNetReact.Data;

public class MvcMovieContext(DbContextOptions<MvcMovieContext> options) : DbContext(options)
{

    public DbSet<Movie> Movie { get; set; } = default!;
}
