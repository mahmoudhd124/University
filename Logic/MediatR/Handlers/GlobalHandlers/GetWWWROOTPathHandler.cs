using Logic.MediatR.Queries.GlobalQueries;
using MediatR;

namespace Logic.MediatR.Handlers.GlobalHandlers;

public class GetWwwrootPathHandler : IRequestHandler<GetWwwrootPathQuery, string>
{
    public Task<string> Handle(GetWwwrootPathQuery request, CancellationToken cancellationToken)
    {
        var dir = string.Join("\\",
                      Environment.CurrentDirectory.Split("\\").SkipLast(1)) + "\\" +
                  typeof(EntryPoint).Assembly.FullName?.Split(",").First() + "\\wwwroot";

        return Task.FromResult(dir);
    }
}