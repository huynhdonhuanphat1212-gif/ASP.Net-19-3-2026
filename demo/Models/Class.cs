using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace demo.Models
{
    public class Class
    {
        public int Id { get; set; }

        [Required]
        public string ClassName { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        [JsonIgnore]
        public ICollection<Student>? Students { get; set; }

        [JsonIgnore]
        public ICollection<Schedule>? Schedules { get; set; }
    }
}