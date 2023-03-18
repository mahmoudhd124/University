using Logic.Models.IdentityModels;

namespace Logic.Models;

public class Doctor : User
{
    public string NationalNumber { get; set; }
    public IList<DoctorSubject> DoctorSubjects { get; set; } = new List<DoctorSubject>();
}