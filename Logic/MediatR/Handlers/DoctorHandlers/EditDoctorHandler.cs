using AutoMapper;
using Logic.Data;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.MediatR.Commands.DoctorCommands;
using Logic.MediatR.Queries.AuthQueries;
using Logic.Models.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Logic.MediatR.Handlers.DoctorHandlers;

public class EditDoctorHandler : IRequestHandler<EditDoctorCommand, Response<bool>>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;
    private readonly IdentityContext _context;

    public EditDoctorHandler(IMapper mapper, IMediator mediator, UserManager<User> userManager, IdentityContext context)
    {
        _mapper = mapper;
        _mediator = mediator;
        _userManager = userManager;
        _context = context;
    }

    public async Task<Response<bool>> Handle(EditDoctorCommand request, CancellationToken cancellationToken)
    {
        var (editDoctorDto, doctorId) = request;
        if (doctorId.Equals(editDoctorDto.Id) == false)
            return Response<bool>.Failure(UserErrors.UnAuthorizedEdit);

        var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id.Equals(doctorId), cancellationToken);
        if (doctor == null)
            return Response<bool>.Failure(UserErrors.WrongId);

        if (editDoctorDto.Email.Equals(doctor.Email) == false)
        {
            var validEmail = await _mediator.Send(new IsValidEmailQuery(editDoctorDto.Email), cancellationToken);
            if (validEmail == false)
                return Response<bool>.Failure(UserErrors.NotValidEmailError);
        }

        _mapper.Map(editDoctorDto, doctor);
        var result = await _userManager.UpdateAsync(doctor);
        if (result.Succeeded)
            return true;

        return Response<bool>.Failure(new Error("User.UnknownError",
            string.Join("\n", result.Errors.Select(e => e.Description))));
    }
}