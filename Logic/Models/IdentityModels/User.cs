using Microsoft.AspNetCore.Identity;

namespace Logic.Models.IdentityModels;

public class User : IdentityUser<string>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Type { get; set; }
}