using AutoMapper;
using Logic.Dtos.AuthDto;
using Logic.Dtos.DoctorDto;
using Logic.Dtos.SubjectDto;
using Logic.Models;
using Logic.Models.IdentityModels;

namespace Logic.Helpers;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<RegisterUserDto, User>();
        CreateMap<RefreshTokenDto, TokenDto>();
        CreateMap<AddDoctorDto, Doctor>();
        CreateMap<EditDoctorDto, Doctor>();
        CreateMap<Doctor, DoctorDto>();
        CreateMap<Doctor, DoctorForPageDto>();
        CreateMap<Subject, SubjectForPageDto>()
            .ForMember(dest => dest.Name, opt =>
                opt.MapFrom(src => src.Department + src.Code));
        CreateMap<AddSubjectDto, Subject>();
        CreateMap<Subject, SubjectDto>()
            .ForMember(dest => dest.Name, opt =>
                opt.MapFrom(src => src.Department + src.Code));
    }
}