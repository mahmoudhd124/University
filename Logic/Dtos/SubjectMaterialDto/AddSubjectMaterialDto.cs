using Logic.Models;

namespace Logic.Dtos.SubjectMaterialDto;

public class AddSubjectMaterialDto
{
    public int SubjectId { get; set; }
    public SubjectFileTypes Type { get; set; }
}