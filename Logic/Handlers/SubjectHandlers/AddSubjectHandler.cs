using AutoMapper;
using Logic.Commands.SubjectCommands;
using Logic.Data;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.Handlers.SubjectHandlers;

public class AddSubjectHandler : IRequestHandler<AddSubjectCommand, Response<bool>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public AddSubjectHandler(IdentityContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<bool>> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
    {
        var addSubjectDto = request.AddSubjectDto;

        var found = await _context.Subjects
            .AnyAsync(s => s.Code == addSubjectDto.Code, cancellationToken);
        if(found)
            return Response<bool>.Failure(SubjectErrors.CodeAlreadyExists);

        _context.Add(_mapper.Map<Subject>(addSubjectDto));
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}