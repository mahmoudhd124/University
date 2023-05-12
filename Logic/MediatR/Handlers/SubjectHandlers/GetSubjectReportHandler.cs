using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logic.Data;
using Logic.Dtos.SubjectDto;
using Logic.ErrorHandlers;
using Logic.MediatR.Queries.SubjectQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.MediatR.Handlers.SubjectHandlers;

public class GetSubjectReportHandler : IRequestHandler<GetSubjectReportQuery, Response<SubjectReportDto>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public GetSubjectReportHandler(IdentityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<SubjectReportDto>> Handle(GetSubjectReportQuery request, CancellationToken cancellationToken)
    {
        var id = request.SubjectId;

        var subjectDto = await _context.Subjects
            .Include(s => s.DoctorSubject)
            .ThenInclude(ds => ds.Doctor)
            .Include(s => s.SubjectFiles)
            .ProjectTo<SubjectReportDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(s => s.Id == id,cancellationToken);

        return subjectDto;
    }
}