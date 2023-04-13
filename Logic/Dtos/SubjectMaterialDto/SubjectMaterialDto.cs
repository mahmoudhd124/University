namespace Logic.Dtos.SubjectMaterialDto;

public class SubjectMaterialDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string StoredName { get; set; }
    public int SubjectId { get; set; }
    public DateTime Date { get; set; }
}