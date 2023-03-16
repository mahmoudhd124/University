using Logic.Dtos.DoctorDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.Commands.DoctorCommands;

public record AddDoctorCommand(AddDoctorDto AddDoctorDto):IRequest<Response<bool>>;