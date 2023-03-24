using Logic.Dtos.DoctorDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Commands.DoctorCommands;

public record EditDoctorCommand(EditDoctorDto EditDoctorDto,string DoctorId):IRequest<Response<bool>>;