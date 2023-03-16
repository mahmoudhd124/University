using AutoMapper;
using Logic.Commands.AuthCommands;
using Logic.Data;
using Logic.Dtos.AuthDto;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.Models.IdentityModels;
using Logic.Queries.AuthQueries;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Logic.Handlers.AuthHandlers;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Response<bool>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IMediator _mediator;

    public RegisterUserHandler(IdentityContext context, IMapper mapper, UserManager<User> userManager,
        IMediator mediator)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _mediator = mediator;
    }


    async Task<Response<bool>> IRequestHandler<RegisterUserCommand, Response<bool>>.Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var registerUserDto = request.RegisterUserDto;
        
        var isValidUsername = await _mediator.Send(new IsValidUsernameQuery(registerUserDto.Username), cancellationToken);
        if(isValidUsername == false)
            return Response<bool>.Failure(UserErrors.UsernameAlreadyUsedError);
        
        var isValidPassword = await _mediator.Send(new IsValidPasswordQuery(registerUserDto.Password), cancellationToken);
        if(isValidPassword == false)
            return Response<bool>.Failure(UserErrors.WeakPasswordError);
        
        var isValidEmail = await _mediator.Send(new IsValidEmailQuery(registerUserDto.Email), cancellationToken);
        if(isValidEmail == false)
            return Response<bool>.Failure(UserErrors.EmailAlreadyUsedError);
        

        var user = _mapper.Map<RegisterUserDto, User>(registerUserDto);
        var result = await _userManager.CreateAsync(user, registerUserDto.Password);

        if (result.Succeeded)
            return true;
        else
            return Response<bool>.Failure(new Error("User.UnknownError",
                string.Join("\n", result.Errors.Select(e => e.Description))));
    }
}