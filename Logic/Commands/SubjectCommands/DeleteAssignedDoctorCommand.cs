using Logic.ErrorHandlers;
using MediatR;

namespace Logic.Commands.SubjectCommands;

public record DeleteAssignedDoctorCommand(int SubjectId) : IRequest<Response<bool>>;