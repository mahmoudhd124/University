using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logic.Data;
using Logic.Dtos.SubjectDto;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.MediatR.Queries.SubjectQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.MediatR.Handlers.SubjectHandlers;

public class GetSubjectByCodeHandler : IRequestHandler<GetSubjectByCodeQuery, Response<SubjectDto>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public GetSubjectByCodeHandler(IdentityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<SubjectDto>> Handle(GetSubjectByCodeQuery request, CancellationToken cancellationToken)
    {
        var (code, roles, id) = request;
        var subjectDto = await _context.Subjects
            .Include(s => s.DoctorSubject)
            .ThenInclude(x => x.Doctor)
            .Include(s => s.SubjectMaterials)
            .AsSplitQuery()
            .ProjectTo<SubjectDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(s => s.Code == code, cancellationToken);

        if (subjectDto == null)
            return Response<SubjectDto>.Failure(SubjectErrors.WrongCode);

        var enumerable = roles.ToList();
        if (enumerable.Contains("Admin") == false && enumerable.Contains("Doctor"))
            if ((subjectDto.DoctorId?.Equals(id) ?? false) == false)
                return Response<SubjectDto>.Failure(SubjectErrors.UnAuthorizedGet);

        subjectDto.IsOwner = subjectDto.DoctorId?.Equals(id) ?? false;

        return subjectDto;
    }
}