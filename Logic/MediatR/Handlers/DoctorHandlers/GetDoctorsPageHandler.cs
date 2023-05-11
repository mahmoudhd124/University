using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logic.Data;
using Logic.Dtos.DoctorDto;
using Logic.ErrorHandlers;
using Logic.MediatR.Queries.DoctorQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.MediatR.Handlers.DoctorHandlers;

public class GetDoctorsPageHandler : IRequestHandler<GetDoctorsPageQuery, Response<IEnumerable<DoctorForPageDto>>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public GetDoctorsPageHandler(IdentityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<DoctorForPageDto>>> Handle(GetDoctorsPageQuery request,
        CancellationToken cancellationToken)
    {
        var (pageSize, pageIndex, usernamePrefix, hasSubject) = request;
        var result = _context.Doctors
            .Where(d => d.UserName.ToUpper().StartsWith(usernamePrefix.ToUpper()))
            .AsQueryable();

        if (hasSubject != null)
            result = result
                .Include(d => d.DoctorSubjects)
                .Where(d => (hasSubject ?? false) ? d.DoctorSubjects.Count > 0 : d.DoctorSubjects.Count == 0);

        return await result
            .ProjectTo<DoctorForPageDto>(_mapper.ConfigurationProvider)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}