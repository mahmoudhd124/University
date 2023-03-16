using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logic.Data;
using Logic.Dtos.DoctorDto;
using Logic.ErrorHandlers;
using Logic.Models;
using Logic.Queries.DoctorQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.Handlers.DoctorHandlers;

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
        var (pageSize, pageIndex, usernamePrefix) = request;

        return await _context.Doctors
            .Where(d => d.UserName.ToUpper().StartsWith(usernamePrefix.ToUpper()))
            .ProjectTo<DoctorForPageDto>(_mapper.ConfigurationProvider)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}