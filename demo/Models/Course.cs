using System.ComponentModel.DataAnnotations;

namespace demo.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string CourseCode { get; set; } = string.Empty;

        [Required]
        public string CourseName { get; set; } = string.Empty;

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}