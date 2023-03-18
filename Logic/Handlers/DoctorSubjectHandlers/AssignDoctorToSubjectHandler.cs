using Logic.Commands.DoctorSubjectCommands;
using Logic.Data;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.Handlers.DoctorSubjectHandlers;

public class AssignDoctorToSubjectHandler : IRequestHandler<AssignDoctorToSubjectCommand, Response<bool>>
{
    private readonly IdentityContext _context;

    public AssignDoctorToSubjectHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(AssignDoctorToSubjectCommand request, CancellationToken cancellationToken)
    {
        var (doctorId, subjectId) = request;
        var doctorFound = await _context.Doctors
            .AnyAsync(d => d.Id.Equals(doctorId), cancellationToken);
        if (doctorFound == false)
            return Response<bool>.Failure(UserErrors.WrongId);

        var subjectFound = await _context.Subjects
            .AnyAsync(d => d.Id == subjectId, cancellationToken);
        if (subjectFound == false)
            return Response<bool>.Failure(SubjectErrors.WrongId);

        var isAssignedSubject = await _context.DoctorSubjects
            .AnyAsync(x => x.SubjectId == subjectId, cancellationToken);
        if(isAssignedSubject)
            return Response<bool>.Failure(SubjectErrors.SubjectIsAlreadyAssignedToDoctor);
            

        _context.DoctorSubjects.Add(new DoctorSubject
        {
            DoctorId = doctorId,
            SubjectId = subjectId
        });
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}