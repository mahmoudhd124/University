using System.Text.RegularExpressions;
using Logic.Queries.AuthQueries;
using MediatR;

namespace Logic.Handlers.AuthHandlers;

public class IsValidPasswordHandler:IRequestHandler<IsValidPasswordQuery,bool>
{
    public Task<bool> Handle(IsValidPasswordQuery request, CancellationToken cancellationToken)
    {
        var pass = request.Password;
        if (string.IsNullOrWhiteSpace(pass))
            return Task.FromResult(false);
        
        var isGoodPass = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

        return Task.FromResult(isGoodPass.IsMatch(pass));
    }
}