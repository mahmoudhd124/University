using Logic.Dtos.AuthDto;
using Logic.ErrorHandlers;
using Logic.Models.IdentityModels;
using MediatR;

namespace Logic.Queries.AuthQueries;

public record GetTokenAndRefreshTokenQuery(User User) : IRequest<Response<RefreshTokenDto>>;