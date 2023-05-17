using Logic.Dtos.SubjectMaterialDto;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.MediatR.Queries.GlobalQueries;
using Logic.MediatR.Queries.SubjectMaterialsQueries;
using Logic.Models;
using MediatR;

namespace Logic.MediatR.Handlers.SubjectMaterialHandlers;

public class GetSubjectFileTypeTemplateHandler : IRequestHandler<GetSubjectFileTypeTemplateQuery,
    Response<GetSubjectMaterialPathAndTypeDto>>
{
    private readonly IMediator _mediator;

    public GetSubjectFileTypeTemplateHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Response<GetSubjectMaterialPathAndTypeDto>> Handle(GetSubjectFileTypeTemplateQuery request,
        CancellationToken cancellationToken)
    {
        var type = request.Type;
        var wwwroot = await _mediator.Send(new GetWwwrootPathQuery(), cancellationToken);
        var path = Path.Combine(wwwroot, "SubjectFileTemplate", type + ".docx");
        try
        {
            return new GetSubjectMaterialPathAndTypeDto()
            {
                Path = path,
                Bytes = await File.ReadAllBytesAsync(path, cancellationToken)
            };
        }
        catch (FileNotFoundException e)
        {
            return Response<GetSubjectMaterialPathAndTypeDto>.Failure(SubjectFileTemplateErrors.FileNotFound);
        }
    }
}