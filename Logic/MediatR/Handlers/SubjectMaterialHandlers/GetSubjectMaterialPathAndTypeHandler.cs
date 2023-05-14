using Logic.Dtos.SubjectMaterialDto;
using Logic.ErrorHandlers;
using Logic.MediatR.Queries.GlobalQueries;
using Logic.MediatR.Queries.SubjectMaterialsQueries;
using MediatR;

namespace Logic.MediatR.Handlers.SubjectMaterialHandlers;

public class GetSubjectMaterialPathAndTypeHandler : IRequestHandler<GetSubjectMaterialPathAndContentQuery,
    Response<GetSubjectMaterialPathAndTypeDto>>
{
    private readonly IMediator _mediator;

    public GetSubjectMaterialPathAndTypeHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Response<GetSubjectMaterialPathAndTypeDto>> Handle(GetSubjectMaterialPathAndContentQuery request,
        CancellationToken cancellationToken)
    {
        var name = request.Name;
        var wwwroot = await _mediator.Send(new GetWwwrootPathQuery(), cancellationToken);
        var path = $"{wwwroot}\\SubjectMaterials\\{name}";
        return new GetSubjectMaterialPathAndTypeDto()
        {
            Path = path,
            Bytes = await File.ReadAllBytesAsync(path, cancellationToken)
        };
    }
}