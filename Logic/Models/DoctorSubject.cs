namespace Logic.Models;

public class DoctorSubject
{
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public Subject Subject { get; set; }
    public string DoctorId { get; set; }
    public Doctor Doctor { get; set; }
}