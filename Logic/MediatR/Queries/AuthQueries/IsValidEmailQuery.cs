using MediatR;

namespace Logic.MediatR.Queries.AuthQueries;

public record IsValidEmailQuery(string Email):IRequest<bool>;