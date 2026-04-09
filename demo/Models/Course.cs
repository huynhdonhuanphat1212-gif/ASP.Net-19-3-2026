using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace demo.Models
{
    public class Course
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        [Required]
        public string CourseName { get; set; }

        public int Credits { get; set; }

        [JsonIgnore]
        public ICollection<Enrollment>? Enrollments { get; set; }

        [JsonIgnore]
        public ICollection<Schedule>? Schedules { get; set; }
    }
}