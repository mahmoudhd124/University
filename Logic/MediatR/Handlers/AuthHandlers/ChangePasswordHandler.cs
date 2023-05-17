using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.MediatR.Commands.AuthCommands;
using Logic.MediatR.Queries.AuthQueries;
using Logic.Models.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Logic.MediatR.Handlers.AuthHandlers;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Response<bool>>
{
    private readonly UserManager<User> _manager;
    private readonly IMediator _mediator;

    public ChangePasswordHandler(UserManager<User> manager, IMediator mediator)
    {
        _manager = manager;
        _mediator = mediator;
    }

    public async Task<Response<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var (userId,changePasswordDto) = request;
        var user = await _manager.FindByIdAsync(userId);

        var isValidPassword = await _mediator.Send(new IsValidPasswordQuery(changePasswordDto.NewPassword), cancellationToken);
        if (isValidPassword == false)
            return Response<bool>.Failure(UserErrors.WeakPasswordError);

        if (user == null)
            return Response<bool>.Failure(UserErrors.WrongId);

        var result = await _manager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
        
        if (result.Succeeded)
            return true;
        
        return Response<bool>.Failure(new Error("User.ChangePasswordError",
            string.Join("\n", result.Errors.Select(e => e.Description))));
    }
}