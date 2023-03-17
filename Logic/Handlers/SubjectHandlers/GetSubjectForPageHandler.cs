using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logic.Data;
using Logic.Dtos.SubjectDto;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.Queries.SubjectQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.Handlers.SubjectHandlers;

public class
    GetSubjectForPageHandler : IRequestHandler<GetSubjectForPageQuery, Response<IEnumerable<SubjectForPageDto>>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public GetSubjectForPageHandler(IdentityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<SubjectForPageDto>>> Handle(GetSubjectForPageQuery request,
        CancellationToken cancellationToken)
    {
        var (pageIndex, pageSize, department, year) = request;

        if (year is < 1 or > 9)
            return Response<IEnumerable<SubjectForPageDto>>.Failure(SubjectErrors.InvalidYear);

        var subjectDtos = _context.Subjects.AsQueryable();

        if (string.IsNullOrWhiteSpace(department) == false)
            subjectDtos = subjectDtos
                .Where(s => s.Department.ToUpper().Equals(department.ToUpper()));

        if (year != null)
            subjectDtos = subjectDtos
                .Where(s => s.Code > (year * 100 - 1) && s.Code < ((year + 1) * 100));

        return await subjectDtos
            .ProjectTo<SubjectForPageDto>(_mapper.ConfigurationProvider)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}