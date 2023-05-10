using Logic.Models;

namespace Logic.Dtos.SubjectMaterialDto;

public class SubjectFileDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string StoredName { get; set; }
    public int SubjectId { get; set; }
    public DateTime Date { get; set; }
    public SubjectFileTypes Type { get; set; }
}