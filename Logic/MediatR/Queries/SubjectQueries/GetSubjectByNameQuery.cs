using Logic.Dtos.SubjectDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Queries.SubjectQueries;

public record GetSubjectByNameQuery(string Name, IEnumerable<string> Roles,string UserId) : IRequest<Response<SubjectDto>>;