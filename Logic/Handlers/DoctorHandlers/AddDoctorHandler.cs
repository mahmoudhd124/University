using AutoMapper;
using Logic.Commands.DoctorCommands;
using Logic.Data;
using Logic.Dtos.DoctorDto;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.Models;
using Logic.Models.IdentityModels;
using Logic.Queries.AuthQueries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Logic.Handlers.DoctorHandlers;

public class AddDoctorHandler : IRequestHandler<AddDoctorCommand, Response<bool>>
{
    private readonly UserManager<User> _userManager;
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AddDoctorHandler(UserManager<User> userManager, IdentityContext context, IMapper mapper,
        IMediator mediator)
    {
        _userManager = userManager;
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<Response<bool>> Handle(AddDoctorCommand request, CancellationToken cancellationToken)
    {
        var registerUserDto = request.AddDoctorDto;

        var isValidUsername =
            await _mediator.Send(new IsValidUsernameQuery(registerUserDto.Username), cancellationToken);
        if (isValidUsername == false)
            return Response<bool>.Failure(UserErrors.UsernameAlreadyUsedError);

        var isValidPassword =
            await _mediator.Send(new IsValidPasswordQuery(registerUserDto.Password), cancellationToken);
        if (isValidPassword == false)
            return Response<bool>.Failure(UserErrors.WeakPasswordError);

        var isValidEmail = await _mediator.Send(new IsValidEmailQuery(registerUserDto.Email), cancellationToken);
        if (isValidEmail == false)
            return Response<bool>.Failure(UserErrors.EmailAlreadyUsedError);

        var foundNationalNumber =
            await _context.Doctors.AnyAsync(d => d.NationalNumber.Equals(registerUserDto.NationalNumber),
                cancellationToken);
        if (foundNationalNumber)
            return Response<bool>.Failure(UserErrors.NationalNumberAlreadyExists);


        var doctor = _mapper.Map<AddDoctorDto, Doctor>(registerUserDto);
        var result = await _userManager.CreateAsync(doctor, registerUserDto.Password);

        if (result.Succeeded == false)
            return Response<bool>.Failure(new Error("User.UnknownError",
                string.Join("\n", result.Errors.Select(e => e.Description))));

        await _userManager.AddToRoleAsync(doctor, "Doctor");
        return true;
    }
}