using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace demo.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }

        public int ClassroomId { get; set; }
        public Classroom? Classroom { get; set; }

        public int ClassId { get; set; }
        public Class? Class { get; set; }

        [Required]
        public string DayOfWeek { get; set; }

        [Required]
        public string TimeSlot { get; set; }

        [JsonIgnore]
        public ICollection<Attendance>? Attendances { get; set; }
    }
}