using MediatR;

namespace Logic.Queries.AuthQueries;

public record IsValidEmailQuery(string Email):IRequest<bool>;