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
        var (addSubjectMaterialDto, fileStream, fileName, userId) = request;

        var subject = await _context.Subjects
            .FirstOrDefaultAsync(s => s.Id == addSubjectMaterialDto.SubjectId, cancellationToken);
        if (subject == null)
            return Response<bool>.Failure(SubjectErrors.WrongId);

        var isAssigned = await _context.DoctorSubjects
            .AnyAsync(x => x.DoctorId.Equals(userId) && x.SubjectId == addSubjectMaterialDto.SubjectId,
                cancellationToken);
        if (isAssigned == false)
            return Response<bool>.Failure(SubjectMaterialErrors.UnAuthorizedAdd);

        // var isTypeFound = await _context.SubjectMaterials
        //     .AnyAsync(s => s.SubjectId == addSubjectMaterialDto.SubjectId && s.Type == addSubjectMaterialDto.Type,
        //         cancellationToken);
        // if (isTypeFound)
        //     return Response<bool>.Failure(SubjectMaterialErrors.RepeatedFileOnTheSameType);

        var newFileName = $"{Guid.NewGuid()}-{userId}-{fileName}";

        var wwwroot = await _mediator.Send(new GetWwwrootPathQuery(), cancellationToken);
        await using var rootFileStream =
            new FileStream($@"{wwwroot}\SubjectMaterials\{newFileName}", FileMode.Create);
        await fileStream.CopyToAsync(rootFileStream, cancellationToken);

        subject.SubjectFiles.Add(new SubjectFiles
        {
            FileName = fileName,
            StoredName = newFileName,
            Type = addSubjectMaterialDto.Type
        });
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}