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

app.Run();