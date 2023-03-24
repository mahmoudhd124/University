using AutoMapper;
using Logic.Dtos.AuthDto;
using Logic.Dtos.DoctorDto;
using Logic.Dtos.SubjectDto;
using Logic.Dtos.SubjectMaterialDto;
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
        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.Subjects, opt =>
                opt.MapFrom(src => src.DoctorSubjects.Select(ds => new SubjectForPageDto
                {
                    Id = ds.SubjectId,
                    Department = ds.Subject.Department,
                    Code = ds.Subject.Code,
                    Name = ds.Subject.Name
                })));
        CreateMap<Doctor, DoctorForPageDto>();
        CreateMap<Subject, SubjectForPageDto>();
        CreateMap<AddSubjectDto, Subject>();
        CreateMap<Subject, SubjectDto>()
            .ForMember(dest => dest.HasADoctor, opt =>
                opt.MapFrom(src => src.DoctorSubject != null))
            .ForMember(dest => dest.DoctorId, opt =>
                opt.MapFrom(src => src.DoctorSubject.DoctorId))
            .ForMember(dest => dest.DoctorUsername, opt =>
                opt.MapFrom(src => src.DoctorSubject.Doctor.UserName))
            .ForMember(dest => dest.Materials, opt =>
                opt.MapFrom(src =>
                    src.SubjectMaterials.Select(x => new SubjectMaterialDto
                    { Name = x.Material, Id = x.Id, Date = x.Date })));
        CreateMap<EditSubjectDto, Subject>();
        CreateMap<SubjectMaterial, SubjectMaterialDto>()
            .ForMember(dest => dest.Name, opt =>
                opt.MapFrom(src => src.Material));
    }
}