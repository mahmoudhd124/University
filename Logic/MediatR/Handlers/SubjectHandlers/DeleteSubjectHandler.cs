using Logic.Data;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.MediatR.Commands.SubjectCommands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.MediatR.Handlers.SubjectHandlers;

public class DeleteSubjectHandler : IRequestHandler<DeleteSubjectCommand, Response<bool>>
{
    private readonly IdentityContext _context;

    public DeleteSubjectHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if(subject == null)
            return Response<bool>.Failure(SubjectErrors.WrongId);
        _context.Subjects.Remove(subject);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}