using Logic.Data;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.MediatR.Commands.RoleCommands;
using Logic.Models.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Logic.MediatR.Handlers.RoleHandlers;

public class AddRoleHandler : IRequestHandler<AddRoleCommand, Response<bool>>
{
    private readonly RoleManager<Role> _roleManager;
    private readonly IdentityContext _context;

    public AddRoleHandler(RoleManager<Role> roleManager, IdentityContext context)
    {
        _roleManager = roleManager;
        _context = context;
    }

    public async Task<Response<bool>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var addRoleDto = request.AddRoleDto;

        var found = await _context.Roles.AnyAsync(r => r.Name.ToUpper().Equals(addRoleDto.Name.ToUpper()),
            cancellationToken);
        if (found)
            return Response<bool>.Failure(RoleErrors.NameAlreadyExists);

        var result = await _roleManager.CreateAsync(new Role
        {
            Name = addRoleDto.Name
        });

        return result.Succeeded
            ? true
            : Response<bool>.Failure(new Error("Role.UnknownError",
                string.Join("\n", result.Errors.Select(e => e.Description))));
    }
}