using Logic.Dtos.SubjectMaterialDto;
using Logic.ErrorHandlers;
using Logic.Models;
using MediatR;

namespace Logic.MediatR.Queries.SubjectMaterialsQueries;

public record GetSubjectFileTypeTemplateQuery(SubjectFileTypes Type):IRequest<Response<GetSubjectMaterialPathAndTypeDto>>;