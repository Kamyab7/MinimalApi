using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Movies.Queries.GetAllMovie;
public class GetAllMovieQuery :IRequest<List<Movie>>
{
}

public class GetAllMovieQueryHandler : IRequestHandler<GetAllMovieQuery, List<Movie>>
{
    private readonly IAppDbContext _appDbContext;

    public GetAllMovieQueryHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Movie>> Handle(GetAllMovieQuery request, CancellationToken cancellationToken)
    {
        return await _appDbContext.Movies.ToListAsync();
    }
}
