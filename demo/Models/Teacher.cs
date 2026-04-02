using System.ComponentModel.DataAnnotations;

namespace demo.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}