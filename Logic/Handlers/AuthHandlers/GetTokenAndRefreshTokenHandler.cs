using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Logic.Data;
using Logic.Dtos.AuthDto;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.Helpers;
using Logic.Models.IdentityModels;
using Logic.Queries.AuthQueries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Logic.Handlers.AuthHandlers;

public class GetTokenAndRefreshTokenHandler : IRequestHandler<GetTokenAndRefreshTokenQuery, Response<RefreshTokenDto>>
{
    private readonly IdentityContext _context;
    private readonly UserManager<User> _userManager;
    private readonly Expiry _expiry;
    private readonly JWT _jwt;

    public GetTokenAndRefreshTokenHandler(IdentityContext context, UserManager<User> userManager, IOptions<JWT> jwt,
        IOptions<Expiry> expiry)
    {
        _context = context;
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
            .Append(new Claim(ClaimTypes.NameIdentifier, user.UserName!));
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
            Expires = DateTime.Now.AddMinutes(_expiry.TokenExpiryInMinutes).ToLocalTime(),
            // Expires = DateTime.Now.AddSeconds(7).ToLocalTime(),
            SigningCredentials = new SigningCredentials(_jwt.SecurityKey, SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }
}