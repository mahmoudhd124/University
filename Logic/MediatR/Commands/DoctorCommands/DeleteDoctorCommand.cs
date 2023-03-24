using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Commands.DoctorCommands;

public record DeleteDoctorCommand(string Id) : IRequest<Response<bool>>;