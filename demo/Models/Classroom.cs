using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace demo.Models
{
    public class Classroom
    {
        public int Id { get; set; }

        [Required]
        public string RoomName { get; set; }

        public int Capacity { get; set; }

        [JsonIgnore]
        public ICollection<Schedule>? Schedules { get; set; }
    }
}