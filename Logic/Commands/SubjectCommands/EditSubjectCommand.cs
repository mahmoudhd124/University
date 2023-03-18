using Logic.Dtos.SubjectDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.Commands.SubjectCommands;

public record EditSubjectCommand(EditSubjectDto EditSubjectDto) : IRequest<Response<bool>>;