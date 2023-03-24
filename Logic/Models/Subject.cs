namespace Logic.Models;

public class Subject
{
    public int Id { get; set; }
    public string Department { get; set; }
    public int Code { get; set; }
    public string Name { get; set; }
    public int Hours { get; set; }
    public DoctorSubject DoctorSubject { get; set; }
    public IList<SubjectMaterial> SubjectMaterials { get; set; } = new List<SubjectMaterial>();
}