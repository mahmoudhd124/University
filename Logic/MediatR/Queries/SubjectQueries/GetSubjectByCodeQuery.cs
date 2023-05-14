using Logic.Dtos.SubjectDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Queries.SubjectQueries;

public record GetSubjectByCodeQuery
    (int Code, IEnumerable<string> Roles, string UserId) : IRequest<Response<SubjectDto>>;