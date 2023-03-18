using Logic.Commands.SubjectCommands;
using Logic.Data;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.Handlers.SubjectHandlers;

public class DeleteAssignedDoctorHandler : IRequestHandler<DeleteAssignedDoctorCommand, Response<bool>>
{
    private readonly IdentityContext _context;

    public DeleteAssignedDoctorHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(DeleteAssignedDoctorCommand request, CancellationToken cancellationToken)
    {
        var id = request.SubjectId;

        var doctorSubject = await _context.DoctorSubjects
            .FirstOrDefaultAsync(x => x.SubjectId == id,cancellationToken);
        
        if(doctorSubject == null)
            return Response<bool>.Failure(SubjectErrors.SubjectIsNotAssignedToDoctor);

        _context.DoctorSubjects.Remove(doctorSubject);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}