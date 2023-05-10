using Logic.Models;

namespace Logic.Dtos.SubjectDto;
using SubjectMaterialDto;

public class SubjectDto
{
    public int Id { get; set; }
    public string Department { get; set; }
    public int Code { get; set; }
    public int Hours { get; set; }
    public string Name { get; set; }
    public bool HasADoctor { get; set; }
    public string DoctorId { get; set; }
    public string DoctorUsername { get; set; }
    public bool IsOwner { get; set; }
    public IList<SubjectFileDto> Files { get; set; }
    public int TotalNumberOfFilesRequired { get; set; } = Enum.GetValues(typeof(SubjectFileTypes)).Length;
}