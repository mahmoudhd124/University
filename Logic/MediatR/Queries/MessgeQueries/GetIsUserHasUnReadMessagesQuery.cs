using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Queries.MessgeQueries;

public record GetIsUserHasUnReadMessagesQuery(string UserId) : IRequest<Response<bool>>;