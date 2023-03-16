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

    public AddSubjectHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
    {
        var addSubjectDto = request.AddSubjectDto;

        var found = await _context.Subjects
            .AnyAsync(s => s.Name.Equals(addSubjectDto.Name), cancellationToken);
        if(found)
            return Response<bool>.Failure(SubjectErrors.NameAlreadyExists);

        _context.Add(new Subject
        {
            Name = addSubjectDto.Name
        });
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}