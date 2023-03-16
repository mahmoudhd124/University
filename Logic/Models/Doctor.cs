using Logic.Models.IdentityModels;

namespace Logic.Models;

public class Doctor : User
{
    public string NationalNumber { get; set; }
}