using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.MediatR.Commands.SubjectMaterialCommands;
using Logic.MediatR.Queries.GlobalQueries;
using MediatR;

namespace Logic.MediatR.Handlers.SubjectMaterialHandlers;

public class AddFileTypeTemplateHandler : IRequestHandler<AddFileTypeTemplateCommand, Response<bool>>
{
    private readonly IMediator _mediator;

    public AddFileTypeTemplateHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Response<bool>> Handle(AddFileTypeTemplateCommand request, CancellationToken cancellationToken)
    {
        var (type, fileStream, fileName) = request;
        var wwwroot = await _mediator.Send(new GetWwwrootPathQuery(), cancellationToken);

        if (fileName.Split(".").Last() != "docx")
            return Response<bool>.Failure(SubjectFileTemplateErrors.UnSupportedSubjectFileTypeTemplateType);

        var path = Path.Combine(wwwroot, "SubjectFileTemplate", type + ".docx");

        File.Delete(path);
        await using var rootFileStream = new FileStream(path, FileMode.CreateNew);
        await fileStream.CopyToAsync(rootFileStream, cancellationToken);
        
        return true;
    }
}