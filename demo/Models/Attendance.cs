using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace demo.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        [JsonIgnore]
        public Student? Student { get; set; }

        public int ScheduleId { get; set; }

        [JsonIgnore]
        public Schedule? Schedule { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public bool IsPresent { get; set; }
    }
}