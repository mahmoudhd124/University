using Logic.ErrorHandlers;
using MediatR;

namespace Logic.Commands.SubjectCommands;

public record DeleteSubjectCommand(int Id) : IRequest<Response<bool>>;