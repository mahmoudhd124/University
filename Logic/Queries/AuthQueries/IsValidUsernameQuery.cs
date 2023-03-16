using MediatR;

namespace Logic.Queries.AuthQueries;

public record IsValidUsernameQuery(string Username):IRequest<bool>;