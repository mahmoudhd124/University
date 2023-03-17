using AutoMapper;
using Logic.Data;
using Logic.Dtos.SubjectDto;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.Queries.SubjectQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.Handlers.SubjectHandlers;

public class GetSubjectByNameHandler : IRequestHandler<GetSubjectByNameQuery, Response<SubjectDto>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public GetSubjectByNameHandler(IdentityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<SubjectDto>> Handle(GetSubjectByNameQuery request, CancellationToken cancellationToken)
    {
        var name = request.Name;
        var subject = await
            _context.Subjects.FirstOrDefaultAsync(
                s => (s.Department.ToUpper() + s.Code.ToString()).Equals(name.ToUpper()), cancellationToken);

        if (subject == null)
            return Response<SubjectDto>.Failure(SubjectErrors.WrongName);

        return _mapper.Map<SubjectDto>(subject);
    }
}