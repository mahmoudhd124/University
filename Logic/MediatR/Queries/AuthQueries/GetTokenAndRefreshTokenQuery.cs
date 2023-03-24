using Logic.Dtos.AuthDto;
using Logic.ErrorHandlers;
using Logic.Models.IdentityModels;
using MediatR;

namespace Logic.MediatR.Queries.AuthQueries;

public record GetTokenAndRefreshTokenQuery(User User) : IRequest<Response<RefreshTokenDto>>;