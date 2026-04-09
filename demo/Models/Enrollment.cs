using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace demo.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public int CourseId { get; set; }

        [JsonIgnore]
        public Student? Student { get; set; }

        [JsonIgnore]
        public Course? Course { get; set; }
    }
}