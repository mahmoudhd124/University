using Logic.Dtos.SubjectDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.Queries.SubjectQueries;

public record GetSubjectByNameQuery(string Name):IRequest<Response<SubjectDto>>;