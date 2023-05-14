using Logic.Dtos.DoctorDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Queries.DoctorQueries;

public record GetDoctorsPageQuery
    (int PageSize, int PageIndex, string UsernamePrefix, bool? HasSubject) : IRequest<
        Response<IEnumerable<DoctorForPageDto>>>;