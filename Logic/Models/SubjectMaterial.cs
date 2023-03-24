namespace Logic.Models;

public class SubjectMaterial
{
    public int Id { get; set; }
    public string StoredName { get; set; }
    public string Material { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public int SubjectId { get; set; }
    public Subject Subject { get; set; }
}