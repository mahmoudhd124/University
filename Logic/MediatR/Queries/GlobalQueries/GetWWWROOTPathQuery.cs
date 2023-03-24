using MediatR;

namespace Logic.MediatR.Queries.GlobalQueries;

public record GetWwwrootPathQuery() : IRequest<string>;