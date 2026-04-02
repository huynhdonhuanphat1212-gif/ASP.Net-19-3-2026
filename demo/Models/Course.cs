using demo.Models;
using System.Text.Json.Serialization;

public class Course
{
    public int Id { get; set; }

    public int TeacherId { get; set; }
    public Teacher? Teacher { get; set; }

    public string CourseName { get; set; }
    public int Credits { get; set; }

    [JsonIgnore]
    public ICollection<Enrollment>? Enrollments { get; set; }

    // 👉 THÊM DÒNG NÀY
    [JsonIgnore]
    public ICollection<Schedule>? Schedules { get; set; }
}