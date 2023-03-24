using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Commands.SubjectCommands;

public record DeleteSubjectCommand(int Id) : IRequest<Response<bool>>;