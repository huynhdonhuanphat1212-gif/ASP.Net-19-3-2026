using demo.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Student
{
    public int Id { get; set; }

    [Required]
    public string StudentCode { get; set; }

    [Required]
    public string FullName { get; set; }

    public DateTime Birthday { get; set; }

    public int ClassId { get; set; }

    public Class? Class { get; set; }

    [JsonIgnore]
    public ICollection<Enrollment>? Enrollments { get; set; }

    [JsonIgnore]
    public ICollection<Attendance>? Attendances { get; set; }
}