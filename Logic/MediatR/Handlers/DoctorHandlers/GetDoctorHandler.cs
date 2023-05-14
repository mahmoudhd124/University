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
        var (id, userRequestId) = request;
        var doctorDto = await _context.Doctors
            .Include(d => d.DoctorSubjects)
            .ThenInclude(ds => ds.Subject)
            .ThenInclude(s => s.SubjectFiles)
            .AsSplitQuery()
            .ProjectTo<DoctorDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(d => d.Id.Equals(id), cancellationToken);

        if (doctorDto == null)
            return Response<DoctorDto>.Failure(UserErrors.WrongId);

        doctorDto.IsOwner = id.Equals(userRequestId);
        return doctorDto;
    }
}