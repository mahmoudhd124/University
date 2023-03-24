using Logic.Dtos.SubjectMaterialDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Commands.SubjectMaterialCommands;

public record AddSubjectMaterialCommand
(int SubjectId, Stream FileStream, string FileName,
    string UserId) : IRequest<Response<bool>>;