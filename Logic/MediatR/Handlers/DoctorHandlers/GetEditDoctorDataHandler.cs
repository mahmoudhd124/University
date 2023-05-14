using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logic.Data;
using Logic.Dtos.DoctorDto;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.MediatR.Queries.DoctorQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.MediatR.Handlers.DoctorHandlers;

public class GetEditDoctorDataHandler : IRequestHandler<GetEditDoctorDataQuery, Response<EditDoctorDto>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public GetEditDoctorDataHandler(IdentityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<EditDoctorDto>> Handle(GetEditDoctorDataQuery request,
        CancellationToken cancellationToken)
    {
        var id = request.Id;
        var dto = await _context.Doctors
            .ProjectTo<EditDoctorDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(d => d.Id.Equals(id), cancellationToken);

        return dto ?? Response<EditDoctorDto>.Failure(UserErrors.WrongId);
    }
}