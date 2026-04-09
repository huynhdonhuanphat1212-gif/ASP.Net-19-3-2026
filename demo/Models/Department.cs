using demo.Models;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
public class Department
{
    public int Id { get; set; }

    [Required]
    public string DepartmentName { get; set; }

    [JsonIgnore]
    public ICollection<Teacher>? Teachers { get; set; }

    [JsonIgnore]
    public ICollection<Class>? Classes { get; set; }
}