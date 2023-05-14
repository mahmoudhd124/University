using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logic.Data;
using Logic.Dtos.DoctorDto;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.MediatR.Queries.DoctorQueries;
using Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.MediatR.Handlers.DoctorHandlers;

public class GetDoctorReportHandler : IRequestHandler<GetDoctorReportQuery, Response<DoctorReportDto>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public GetDoctorReportHandler(IdentityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<DoctorReportDto>> Handle(GetDoctorReportQuery request,
        CancellationToken cancellationToken)
    {
        var id = request.DocId;

        var doctorReportDto = await _context.Doctors
            .Include(d => d.DoctorSubjects)
            .ThenInclude(ds => ds.Subject)
            .ProjectTo<DoctorReportDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(d => d.Id.Equals(id), cancellationToken);

        if (doctorReportDto == null)
            return Response<DoctorReportDto>.Failure(UserErrors.WrongId);

        var enumValues = Enum.GetValues<SubjectFileTypes>();

        doctorReportDto.IsComplete = doctorReportDto.Subjects
            .Select(s => s.CompletedTypes)
            .Count(ts => ts.Count == enumValues.Length) == doctorReportDto.Subjects.Count;


        foreach (var subject in doctorReportDto.Subjects)
        {
            subject.UnCompletedTypes = new List<SubjectFileTypes>();
            foreach (var val in enumValues)
                if (subject.CompletedTypes.Contains(val) == false)
                    subject.UnCompletedTypes.Add(val);
        }

        return doctorReportDto;
    }
}