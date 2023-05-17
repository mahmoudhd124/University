using Logic.ErrorHandlers;
using Logic.Models;
using MediatR;

namespace Logic.MediatR.Commands.SubjectMaterialCommands;

public record AddFileTypeTemplateCommand
    (SubjectFileTypes Type, Stream FileStream, string FileName) : IRequest<Response<bool>>;