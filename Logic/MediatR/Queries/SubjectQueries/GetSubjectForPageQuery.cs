using Logic.Dtos.SubjectDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Queries.SubjectQueries;

public record GetSubjectForPageQuery
    (int PageIndex, int PageSize, string Department, int? Year) : IRequest<Response<IEnumerable<SubjectForPageDto>>>;