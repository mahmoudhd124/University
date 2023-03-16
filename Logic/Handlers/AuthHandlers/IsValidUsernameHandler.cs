using Logic.Data;
using Logic.Queries.AuthQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.Handlers.AuthHandlers;

public class IsValidUsernameHandler : IRequestHandler<IsValidUsernameQuery, bool>
{
    private readonly IdentityContext _context;

    public IsValidUsernameHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(IsValidUsernameQuery request, CancellationToken cancellationToken)
    {
        var username = request.Username;
        if (string.IsNullOrWhiteSpace(username))
            return false;
        
        var found = await _context.Users.AnyAsync(u => u.UserName.ToUpper().Equals(username.ToUpper()),
            cancellationToken);

        return found == false;
    }
}