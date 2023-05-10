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
        CreateMap<EditDoctorDto, Doctor>().ReverseMap();
        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.Subjects, opt =>
                opt.MapFrom(src => src.DoctorSubjects.Select(ds => new SubjectForPageDto
                {
                    Id = ds.SubjectId,
                    Department = ds.Subject.Department,
                    Code = ds.Subject.Code,
                    Name = ds.Subject.Name,
                    NumberOfFiles = ds.Subject.SubjectFiles.Count
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
            .ForMember(dest => dest.Files, opt =>
                opt.MapFrom(src =>
                    src.SubjectFiles.Select(x => new SubjectFileDto
                    {
                        Name = x.FileName,
                        Id = x.Id,
                        Date = x.Date,
                        StoredName = x.StoredName,
                        Type = x.Type,
                        SubjectId = x.SubjectId
                    })));
        CreateMap<EditSubjectDto, Subject>();
        CreateMap<SubjectFiles, SubjectFileDto>()
            .ForMember(dest => dest.Name, opt =>
                opt.MapFrom(src => src.FileName));
    }
}