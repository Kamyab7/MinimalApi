using Application.Common.Interfaces;
using MediatR;

namespace Application.Movies.Commands.DeleteMovie;
public class DeleteMovieCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, Unit>
{
    private readonly IAppDbContext _appDbContext;

    public DeleteMovieCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var entity = await _appDbContext.Movies.FindAsync(request.Id);

        if (entity==null)
        {
            //TODO: Write NotFound Custom Exeption
            throw new Exception();
        }

        _appDbContext.Movies.Remove(entity);

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
