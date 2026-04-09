using System.ComponentModel.DataAnnotations;

namespace demo.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        // 👉 optional (nâng cấp sau)
        public string? Role { get; set; }
    }
}