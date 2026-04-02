using demo.Models;
using System.Text.Json.Serialization;

public class Student
{
    public int Id { get; set; }
    public string StudentCode { get; set; }
    public string FullName { get; set; }
    public DateTime Birthday { get; set; }

    public int ClassId { get; set; }
    public Class? Class { get; set; }

    [JsonIgnore]
    public ICollection<Enrollment>? Enrollments { get; set; }

    // 👉 THÊM DÒNG NÀY
    [JsonIgnore]
    public ICollection<Attendance>? Attendances { get; set; }
}