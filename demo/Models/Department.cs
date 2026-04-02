using System.ComponentModel.DataAnnotations;

namespace demo.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        public ICollection<Teacher>? Teachers { get; set; }
        public ICollection<Class>? Classes { get; set; }
    }
}