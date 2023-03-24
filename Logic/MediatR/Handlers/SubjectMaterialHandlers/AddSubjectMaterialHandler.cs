using Logic.Data;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.MediatR.Commands.SubjectMaterialCommands;
using Logic.MediatR.Queries.GlobalQueries;
using Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.MediatR.Handlers.SubjectMaterialHandlers;

public class AddSubjectMaterialHandler : IRequestHandler<AddSubjectMaterialCommand, Response<bool>>
{
    private readonly IdentityContext _context;
    private readonly IMediator _mediator;

    public AddSubjectMaterialHandler(IdentityContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Response<bool>> Handle(AddSubjectMaterialCommand request, CancellationToken cancellationToken)
    {
        var (subjectId, fileStream, fileName, userId) = request;

        var subject = await _context.Subjects
            .FirstOrDefaultAsync(s => s.Id == subjectId, cancellationToken);
        if (subject == null)
            return Response<bool>.Failure(SubjectErrors.WrongId);

        var isAssigned = await _context.DoctorSubjects
            .AnyAsync(x => x.DoctorId.Equals(userId) && x.SubjectId == subjectId,
                cancellationToken);
        if (isAssigned == false)
            return Response<bool>.Failure(SubjectMaterialErrors.UnAuthorizedAdd);

        var newFileName = $"{Guid.NewGuid()}-{userId}-{fileName}";
        subject.SubjectMaterials.Add(new SubjectMaterial
        {
            Material = fileName,
            StoredName = newFileName
        });
        await _context.SaveChangesAsync(cancellationToken);

        var wwwroot = await _mediator.Send(new GetWwwrootPathQuery(), cancellationToken);
        await using var rootFileStream =
            new FileStream($@"{wwwroot}\SubjectMaterials\{newFileName}", FileMode.Create);
        await fileStream.CopyToAsync(rootFileStream, cancellationToken);

        return true;
    }
}