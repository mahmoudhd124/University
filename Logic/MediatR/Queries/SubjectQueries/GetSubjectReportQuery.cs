using Logic.Dtos.SubjectDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Queries.SubjectQueries;

public record GetSubjectReportQuery(int SubjectId) : IRequest<Response<SubjectReportDto>>;