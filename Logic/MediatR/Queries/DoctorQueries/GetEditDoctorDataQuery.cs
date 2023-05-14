using Logic.Dtos.DoctorDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Queries.DoctorQueries;

public record GetEditDoctorDataQuery(string Id) : IRequest<Response<EditDoctorDto>>;