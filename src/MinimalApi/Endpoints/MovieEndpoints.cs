using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Dtos;

namespace MinimalApi.Endpoints;

public static class MovieEndpoints
{
    public static WebApplication AddMovieEndpoints(this WebApplication app)
    {
        app.MapGet("api/v1/movies", async (IAppDbContext context) => {
            var movies = await context.Movies.ToListAsync();
            return Results.Ok(movies);
        });

        app.MapGet("api/v1/movies/{id}", async (IAppDbContext context, int id) => {
            var movie = await context.Movies.FindAsync(id);
            if (movie != null)
            {
                return Results.Ok(movie);
            }
            return Results.NotFound();
        });

        app.MapPost("api/v1/movies", async (IAppDbContext context, MovieDto MovieDto, CancellationToken cancellationToken) => {

            var movie = new Movie()
            {
                title = MovieDto.title,
                description = MovieDto.description,
                CreatedDate = MovieDto.CreatedDate
            };

            await context.Movies.AddAsync(movie);

            await context.SaveChangesAsync(cancellationToken);

            return Results.Created($"api/v1/commands/{movie.id}", MovieDto);

        });

        app.MapPut("api/v1/movies/{id}", async (IAppDbContext context, int id, MovieDto MovieDto, CancellationToken cancellationToken) => {
            var movie = await context.Movies.FindAsync(id);
            if (movie == null)
            {
                return Results.NotFound();
            }

            movie.title = MovieDto.title;
            movie.description = MovieDto.description;
            movie.CreatedDate = MovieDto.CreatedDate;

            await context.SaveChangesAsync(cancellationToken);

            return Results.NoContent();
        });

        app.MapDelete("api/v1/movies/{id}", async (IAppDbContext context, int id, CancellationToken cancellationToken) => {
            var movie = await context.Movies.FindAsync(id);
            if (movie == null)
            {
                return Results.NotFound();
            }

            context.Movies.Remove(movie);

            await context.SaveChangesAsync(cancellationToken);

            return Results.NoContent();

        });

        return app;
    }
}
