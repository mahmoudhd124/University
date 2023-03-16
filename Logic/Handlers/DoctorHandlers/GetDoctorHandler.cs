using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logic.Data;
using Logic.Dtos.DoctorDto;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.Queries.DoctorQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.Handlers.DoctorHandlers;

public class GetDoctorHandler : IRequestHandler<GetDoctorQuery, Response<DoctorDto>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public GetDoctorHandler(IdentityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<DoctorDto>> Handle(GetDoctorQuery request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var doctorDto = await _context.Doctors
            .ProjectTo<DoctorDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(d => d.Id.Equals(id), cancellationToken);
        
        return doctorDto ?? Response<DoctorDto>.Failure(UserErrors.WrongId);
    }
}