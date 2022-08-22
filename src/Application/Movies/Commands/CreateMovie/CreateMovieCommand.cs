using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Movies.Commands.CreateMovie;

public class CreateMovieCommand : IRequest<int>
{
    public string title { get; set; }

    public string description { get; set; }

    public DateTime CreatedDate { get; set; }
}

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public CreateMovieCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var entity = new Movie()
        {
            CreatedDate = request.CreatedDate,
            title = request.title,
            description = request.description
        };

        _appDbContext.Movies.Add(entity);

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return entity.id;
    }
}
