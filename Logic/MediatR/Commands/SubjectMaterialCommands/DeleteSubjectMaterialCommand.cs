using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Commands.SubjectMaterialCommands;

public record DeleteSubjectMaterialCommand(int MaterialId, string UserId) : IRequest<Response<bool>>;