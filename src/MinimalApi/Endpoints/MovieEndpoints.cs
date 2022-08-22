using Application.Common.Interfaces;
using Application.Movies.Commands.CreateMovie;
using Application.Movies.Commands.DeleteMovie;
using Application.Movies.Commands.UpdateMovie;
using Application.Movies.Queries.GetAllMovie;
using Application.Movies.Queries.GetMovieById;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Dtos;

namespace MinimalApi.Endpoints;

public static class MovieEndpoints
{
    public static WebApplication AddMovieEndpoints(this WebApplication app)
    {
        app.MapGet("api/v1/movies", async (IMediator mediator) => {
            return await mediator.Send(new GetAllMovieQuery());
        });

        app.MapGet("api/v1/movies/{id}", async(int id,IMediator mediator) => {
            return await mediator.Send(new GetMovieByIdQuery() {Id=id });
        });

        app.MapPost("api/v1/movies", async (CreateMovieCommand command,IMediator mediator) => {
            return await mediator.Send(command);
        });

        app.MapPut("api/v1/movies/{id}", async (int id, UpdateMovieCommand command, IMediator mediator) => {
            return await mediator.Send(command);
        });

        app.MapDelete("api/v1/movies/{id}", async (int id,IMediator mediator) => {
            return await mediator.Send(new DeleteMovieCommand() {Id=id });
        });

        return app;
    }
}
