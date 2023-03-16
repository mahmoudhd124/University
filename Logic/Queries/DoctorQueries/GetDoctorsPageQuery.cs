using Logic.Dtos.DoctorDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.Queries.DoctorQueries;

public record GetDoctorsPageQuery
    (int PageSize, int PageIndex, string UsernamePrefix) : IRequest<Response<IEnumerable<DoctorForPageDto>>>;