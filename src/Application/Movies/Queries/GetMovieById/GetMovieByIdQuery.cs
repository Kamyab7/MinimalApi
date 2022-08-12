using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Movies.Queries.GetMovieById;
public class GetMovieByIdQuery : IRequest<Movie>
{
    public int Id { get; set; }
}

public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, Movie>
{
    private readonly IAppDbContext _appDbContext;

    public GetMovieByIdQueryHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Movie> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _appDbContext.Movies.FindAsync(request.Id);

        if (entity == null)
        {
            //TODO: Wrire NotFound Custom Exeption
            throw new Exception();
        }

        return entity;
    }
}
