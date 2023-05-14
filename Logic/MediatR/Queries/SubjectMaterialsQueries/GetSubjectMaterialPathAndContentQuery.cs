using Logic.Dtos.SubjectMaterialDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Queries.SubjectMaterialsQueries;

public record GetSubjectMaterialPathAndContentQuery(string Name) : IRequest<Response<GetSubjectMaterialPathAndTypeDto>>;