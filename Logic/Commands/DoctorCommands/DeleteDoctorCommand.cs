using Logic.ErrorHandlers;
using MediatR;

namespace Logic.Commands.DoctorCommands;

public record DeleteDoctorCommand(string Id):IRequest<Response<bool>>;