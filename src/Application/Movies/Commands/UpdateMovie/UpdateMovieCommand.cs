using Application.Common.Interfaces;
using MediatR;

namespace Application.Movies.Commands.UpdateMovie;
public class UpdateMovieCommand :IRequest<Unit>
{
    public int Id { get; set; }

    public string title { get; set; }

    public string description { get; set; }

    public DateTime CreatedDate { get; set; }
}

public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Unit>
{
    private readonly IAppDbContext _appDbContext;

    public UpdateMovieCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Unit> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var entity =await _appDbContext.Movies.FindAsync(request.Id);

        if (entity == null)
        {
            //TODO: Write NotFound Custom Exeption
            throw new Exception();
        }

        entity.title = request.title;
        entity.description = request.description;
        entity.CreatedDate = request.CreatedDate;

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
