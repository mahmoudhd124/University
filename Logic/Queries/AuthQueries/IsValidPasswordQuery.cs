using MediatR;

namespace Logic.Queries.AuthQueries;

public record IsValidPasswordQuery(string Password):IRequest<bool>;