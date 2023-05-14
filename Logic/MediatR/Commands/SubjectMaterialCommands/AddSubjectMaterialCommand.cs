using Logic.Dtos.SubjectMaterialDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Commands.SubjectMaterialCommands;

public record AddSubjectMaterialCommand
(AddSubjectMaterialDto AddSubjectMaterialDto, Stream FileStream, string FileName,
    string UserId) : IRequest<Response<bool>>;