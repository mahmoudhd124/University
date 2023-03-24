using MediatR;

namespace Logic.MediatR.Queries.AuthQueries;

public record IsValidUsernameQuery(string Username) : IRequest<bool>;