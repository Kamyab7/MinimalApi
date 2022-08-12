using Microsoft.EntityFrameworkCore;
using MinimalApi.Entities;

namespace MinimalApi.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> option):base(option)
    {

    }

    public DbSet<Movie> Movies { get; set; }
}
