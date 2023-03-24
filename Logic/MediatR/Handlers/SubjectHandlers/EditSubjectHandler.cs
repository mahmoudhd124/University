using AutoMapper;
using Logic.Data;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.MediatR.Commands.SubjectCommands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.MediatR.Handlers.SubjectHandlers;

public class EditSubjectHandler : IRequestHandler<EditSubjectCommand, Response<bool>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public EditSubjectHandler(IdentityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<bool>> Handle(EditSubjectCommand request, CancellationToken cancellationToken)
    {
        var editSubjectDto = request.EditSubjectDto;

        var subject = await _context.Subjects
            .FirstOrDefaultAsync(s => s.Id == editSubjectDto.Id, cancellationToken);
        if (subject == null)
            return Response<bool>.Failure(SubjectErrors.WrongId);

        var sameCodeFound = await _context.Subjects
            .AnyAsync(s => s.Code == editSubjectDto.Code, cancellationToken);
        if (sameCodeFound)
            return Response<bool>.Failure(SubjectErrors.CodeAlreadyExists);

        var sameNameFound = await _context.Subjects
            .AnyAsync(s => s.Name.ToUpper().Equals(editSubjectDto.Name.ToUpper()), cancellationToken);
        if (sameNameFound)
            return Response<bool>.Failure(SubjectErrors.NameAlreadyExists);

        _mapper.Map(editSubjectDto,subject);
        _context.Subjects.Update(subject);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}