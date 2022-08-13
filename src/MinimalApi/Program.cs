using Microsoft.EntityFrameworkCore;
using MinimalApi.Database;
using MinimalApi.Dtos;
using MinimalApi.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("MinimalApiDb"));

builder.Services.AddScoped<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapGet("api/v1/movies", async (AppDbContext context) => {
    var movies = await context.Movies.ToListAsync();
    return Results.Ok(movies);
});

app.MapGet("api/v1/movies/{id}", async (AppDbContext context, int id) => {
    var movie = await context.Movies.FindAsync(id);
    if (movie != null)
    {
        return Results.Ok(movie);
    }
    return Results.NotFound();
});

app.MapPost("api/v1/movies", async (AppDbContext context, MovieDto MovieDto) => {

    var movie = new Movie()
    {
        title = MovieDto.title,
        description = MovieDto.description,
        CreatedDate = MovieDto.CreatedDate
    };

    await context.Movies.AddAsync(movie);

    await context.SaveChangesAsync();

    return Results.Created($"api/v1/commands/{movie.id}", MovieDto);

});

app.MapPut("api/v1/movies/{id}", async (AppDbContext context,int id, MovieDto MovieDto) => {
    var movie = await context.Movies.FindAsync(id);
    if (movie == null)
    {
        return Results.NotFound();
    }

    movie.title = MovieDto.title;
    movie.description = MovieDto.description;
    movie.CreatedDate = MovieDto.CreatedDate;

    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("api/v1/movies/{id}", async (AppDbContext context,int id) => {
    var movie = await context.Movies.FindAsync(id);
    if (movie == null)
    {
        return Results.NotFound();
    }

    context.Movies.Remove(movie);

    await context.SaveChangesAsync();

    return Results.NoContent();

});

app.MapFallbackToFile("index.html"); ;

app.Run();