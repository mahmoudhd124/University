using MediatR;

namespace Logic.MediatR.Queries.AuthQueries;

public record IsValidPasswordQuery(string Password) : IRequest<bool>;