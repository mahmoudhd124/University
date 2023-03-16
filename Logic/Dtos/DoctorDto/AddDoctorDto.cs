using Logic.Dtos.AuthDto;

namespace Logic.Dtos.DoctorDto;

public class AddDoctorDto : RegisterUserDto
{
    public string NationalNumber { get; set; }
}