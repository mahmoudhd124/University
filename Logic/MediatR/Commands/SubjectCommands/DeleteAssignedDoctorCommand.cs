using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Commands.SubjectCommands;

public record DeleteAssignedDoctorCommand(int SubjectId) : IRequest<Response<bool>>;