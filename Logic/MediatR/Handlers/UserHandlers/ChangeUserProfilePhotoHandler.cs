using Logic.Data;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.MediatR.Commands.UserCommands;
using Logic.MediatR.Queries.GlobalQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.MediatR.Handlers.UserHandlers;

public class ChangeUserProfilePhotoHandler : IRequestHandler<ChangeUserProfilePhotoCommand, Response<bool>>
{
    private readonly IdentityContext _context;
    private readonly IMediator _mediator;

    public ChangeUserProfilePhotoHandler(IdentityContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Response<bool>> Handle(ChangeUserProfilePhotoCommand request, CancellationToken cancellationToken)
    {
        var (id, name, stream) = request;
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id.Equals(id), cancellationToken);

        if (user == null)
            return Response<bool>.Failure(UserErrors.WrongId);

        var newImg = $"{id}-{name}";

        var wwwroot = await _mediator.Send(new GetWwwrootPathQuery(), cancellationToken);

        if (user.ProfilePhoto.Equals("default.png") == false)
            File.Delete(Path.Combine(wwwroot, "ProfileImages", user.ProfilePhoto));

        var path = Path.Combine(wwwroot, "ProfileImages", newImg);
        await using var imagesPathStream = new FileStream(path, FileMode.Create);
        await stream.CopyToAsync(imagesPathStream, cancellationToken);

        user.ProfilePhoto = newImg;
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}