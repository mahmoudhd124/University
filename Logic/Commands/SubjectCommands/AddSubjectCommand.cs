using Logic.Dtos.SubjectDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.Commands.SubjectCommands;

public record AddSubjectCommand(AddSubjectDto AddSubjectDto):IRequest<Response<bool>>;