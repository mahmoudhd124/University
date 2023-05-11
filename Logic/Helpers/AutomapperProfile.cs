using AutoMapper;
using Logic.Dtos.AuthDto;
using Logic.Dtos.DoctorDto;
using Logic.Dtos.MessageDto;
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
                    NumberOfFilesTypes = ds.Subject.SubjectFiles
                        .Select(s => s.Type)
                        .Distinct()
                        .Count()
                })))
            .ForMember(dest => dest.IsComplete, opt =>
                opt.MapFrom(src => src.DoctorSubjects
                        .Select(ds => ds.Subject.SubjectFiles.Select(sf => sf.Type))
                        .Select(t => t.Distinct().Count() == Enum.GetNames(typeof(SubjectFileTypes)).Length)
                        .Any(r => r == false) == false
                ));
        
        CreateMap<Doctor, DoctorForPageDto>()
            .ForMember(dest => dest.IsComplete, opt =>
                opt.MapFrom(src => src.DoctorSubjects
                        .Select(ds => ds.Subject.SubjectFiles.Select(sf => sf.Type))
                        .Select(t => t.Distinct().Count() == Enum.GetNames(typeof(SubjectFileTypes)).Length)
                        .Any(r => r == false) == false
                ));

        CreateMap<Subject, SubjectForPageDto>()
            .ForMember(dest => dest.NumberOfFilesTypes, opt =>
                opt.MapFrom(src => src.SubjectFiles
                    .Select(s => s.Type)
                    .Distinct()
                    .Count()
                ));
        
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

        CreateMap<Message, MessageDto>();
        CreateMap<Message, MessageForReceivedListDto>();
        CreateMap<Message, MessageForSendListDto>();
    }
}