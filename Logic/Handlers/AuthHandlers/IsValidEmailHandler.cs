using Logic.Data;
using Logic.Queries.AuthQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.Handlers.AuthHandlers;

public class IsValidEmailHandler : IRequestHandler<IsValidEmailQuery, bool>
{
    private readonly IdentityContext _context;

    public IsValidEmailHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(IsValidEmailQuery request, CancellationToken cancellationToken)
    {
        var email = request.Email;
        return await _context.Users.AnyAsync(u => u.Email.ToUpper().Equals(email.ToUpper()), cancellationToken) == false;
    }
}