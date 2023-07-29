using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Logic.Dtos.AuthDto;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.Helpers;
using Logic.MediatR.Queries.AuthQueries;
using Logic.Models.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Logic.MediatR.Handlers.AuthHandlers;

public class GetTokenAndRefreshTokenHandler : IRequestHandler<GetTokenAndRefreshTokenQuery, Response<RefreshTokenDto>>
{
    private readonly UserManager<User> _userManager;
    private readonly Expiry _expiry;
    private readonly Jwt _jwt;

    public GetTokenAndRefreshTokenHandler(UserManager<User> userManager, IOptions<Jwt> jwt,
        IOptions<Expiry> expiry)
    {
        _userManager = userManager;
        _expiry = expiry.Value;
        _jwt = jwt.Value;
    }

    public async Task<Response<RefreshTokenDto>> Handle(GetTokenAndRefreshTokenQuery request,
        CancellationToken cancellationToken)
    {
        var user = request.User;
        if (user == null)
            return Response<RefreshTokenDto>.Failure(UserErrors.UnknownError);

        if (string.IsNullOrWhiteSpace(user.UserName))
            return Response<RefreshTokenDto>.Failure(UserErrors.WrongUsername);

        var (roles, claims) = await GetRolesAndClaimsAsync(user);
        var refreshToken = GenerateRefreshToken();
        var token = GenerateToken(claims);

        return new RefreshTokenDto
        {
            RefreshToken = refreshToken,
            Token = token,
            Roles = roles,
            UserId = user.Id
        };
    }

    private async Task<(IEnumerable<string>, IEnumerable<Claim>)> GetRolesAndClaimsAsync(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = roles
            .Select(r => new Claim(ClaimTypes.Role, r))
            .Append(new Claim(ClaimTypes.NameIdentifier, user.UserName!))
            .Append(new Claim(ClaimTypes.Sid, user.Id!));
        return (roles, claims);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private string GenerateToken(IEnumerable<Claim> claims)
    {
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_expiry.TokenExpiryInMinutes),
            // Expires = DateTime.Now.AddSeconds(5),
            SigningCredentials = new SigningCredentials(_jwt.SecurityKey, SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }
}