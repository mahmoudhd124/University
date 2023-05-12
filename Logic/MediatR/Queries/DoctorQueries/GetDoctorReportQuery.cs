using Logic.Dtos.DoctorDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Queries.DoctorQueries;

public record GetDoctorReportQuery(string DocId):IRequest<Response<DoctorReportDto>>;